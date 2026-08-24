using Mesenger.Api.DTO.RequestClasses;
using Mesenger.Api.DTO.Transformers;
using Mesenger.Api.Services.Interfaces;
using Messanger.Api.Enums;
using Messanger.Api.ViewModels;
using Messanger.DataAccess.Enums;
using Messanger.DataAccess.Models;
using Messenger.Repository.Interfaces;
using System;

namespace Mesenger.Api.Services
{
    public class ChatService : IChatService
    { 
        private readonly IUserRepository _UserRepository;
        private readonly IChatRepository _ChatRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const int MinCountGroupMembers = 3;

        public ChatService(IUserRepository UserRepository, IChatRepository ChatRepository, IHttpContextAccessor httpContextAccessor)
        {
            _UserRepository = UserRepository;
            _ChatRepository = ChatRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<(Result, ChatViewModel)> CreatePrivateChat(PrivateChatRequest PrivateRequest)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return (new Result(EResultCode.DbError, ""), null!);

            var MainId = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
            var ResultCode = CreatePrivateChatValidation(PrivateRequest);

            if(ResultCode.Item1 == EResultCode.Success)
            {
                var usersToChat = new List<User>();
                usersToChat.Add(_UserRepository.GetByIdAsync(MainId).Result);
                usersToChat.Add(_UserRepository.GetByIdAsync(PrivateRequest.UserId).Result);
                var chat = new Chat() { ChatType = EChatType.Personal, CreatedAt = DateTime.Now, Users = usersToChat };
                _UserRepository.GetByIdAsync(MainId).Result.Chats.Add(chat);
                _UserRepository.GetByIdAsync(PrivateRequest.UserId).Result.Chats.Add(chat);
                await _ChatRepository.AddAsync(chat);
                return (new Result(EResultCode.Success, "Успешно"), ChatDTO.ChatToViewModel(chat)); 
            }
            else if(ResultCode.Item1 == EResultCode.ThisRoomAlreadyExist)
            {
                return (new Result(EResultCode.ThisRoomAlreadyExist, "Комната уже существует"), ResultCode.Item2);
            }
            else
            {
                return (new Result(EResultCode.Error, "Неизвестная ошибка"), new ChatViewModel());
            }
        }

        public async Task<(Result, ChatViewModel)> CreateGroupChat(GroupChatRequest GroupRequest)
        {
            var result = CreateGroupChatValidation(GroupRequest);

            var httpContext = _httpContextAccessor.HttpContext;
            var Users = _UserRepository.GetAsync().Result;
            if (httpContext == null)
                return (new Result(EResultCode.DbError, "лолдбдэррйоу"), default!);

            var MainId = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
            var GroupUsers = new List<User>();
            GroupUsers.Add(Users[MainId]);
            foreach (var UserId in GroupRequest.UsersId)
            {
                GroupUsers.Add(Users.First(u => u.Id == UserId));
            }
            Chat GroupChat = new Chat
            {
                Name = GroupRequest.Name,
                Admin = Users[0],
                ChatType = EChatType.Group,
                CreatedAt = DateTime.Now,
                Users = GroupUsers
            };
            for(int i = 0; i < GroupChat.Users.Count; i++)
            {
                GroupChat.Users[i].Chats.Add(GroupChat);
            }
            await _ChatRepository.AddAsync(GroupChat);
 
            return (new Result(EResultCode.Success, "Успешно"), ChatDTO.ChatToViewModel(GroupChat));
        }


        public async Task<(Result, List<ChatViewModel>)> GetChats()
        {

            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return (new Result(EResultCode.DbError, "дбдэррбро"), null);

            var user = _UserRepository.GetByIdAsync(Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value));

            var chats = user.Result.Chats;
            
            return (new Result(EResultCode.Success, "Успешно"), ChatDTO.ChatsToViewModel(chats));
        }

        public async Task<(Result, ChatViewModel)> GetChatById(int Id)
        {

            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return (new Result(EResultCode.DbError, "дбдэррбро"), null);

            var user = _UserRepository.GetByIdAsync(Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value)).Result;

            var chat = user.Chats.First(c => c.Id == Id); 

            return (new Result(EResultCode.Success, "Успешно"), ChatDTO.ChatToViewModel(chat));
        }

        public (EResultCode, ChatViewModel) CreatePrivateChatValidation(PrivateChatRequest PrivateRequest)
        { 

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return (EResultCode.DbError, null!);
            var MainId = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
            var Users = _UserRepository.GetAsync().Result;
            var Chats = _ChatRepository.GetAsync().Result;
            if (Users[PrivateRequest.UserId] != null) 
                return (EResultCode.NotExist, null!); 
            if (MainId != PrivateRequest.UserId)
                return (EResultCode.Error, null!);

            if(false)//haveNotPermission
                return (EResultCode.HasNotPermission, new ChatViewModel());
            var IsExist = Chats.Where(c => c.Users.Contains(Users[MainId]) && c.Users.Contains(Users[PrivateRequest.UserId])).FirstOrDefault();
            if(IsExist != null)
                return (EResultCode.ThisRoomAlreadyExist, ChatDTO.ChatToViewModel(IsExist));
            return (EResultCode.Success, null!);
        }

        public EResultCode CreateGroupChatValidation(GroupChatRequest GroupRequest) 
        {
            if (string.IsNullOrWhiteSpace(GroupRequest.Name))
            {
                return EResultCode.SomeFieldsEmpty;
            }
            var users = _UserRepository.GetAsync().Result;

            var existedUsers = users.All(u => GroupRequest.UsersId.Contains(u.Id));
            if (!existedUsers)
                return EResultCode.NotExist;

            if (GroupRequest.UsersId.Count < MinCountGroupMembers)
                return EResultCode.Error;

            return EResultCode.Success;
        }
    }
} 