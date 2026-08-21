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
        public async Task<(EResultCode, ChatViewModel)> CreatePrivateChat(int Id)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return (EResultCode.DbError, new ChatViewModel());

            var MainId = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
            var ResultCode = CreatePrivateChatValidation(Id);

            if( ResultCode == EResultCode.Success)
            {
                var usersToChat = new List<User>();
                usersToChat.Add(_UserRepository.GetByIdAsync(MainId).Result);
                usersToChat.Add(_UserRepository.GetByIdAsync(Id).Result);
                var chat = new Chat() { ChatType = EChatType.Personal, CreatedAt = DateTime.Now, Users = usersToChat };
                await _ChatRepository.AddAsync(chat);
                return (EResultCode.Success, ChatDTO.ChatToViewModel(chat)); 
            }
            else if(ResultCode == EResultCode.ThisRoomAlreadyExist)
            {
                return (EResultCode.ThisRoomAlreadyExist, new ChatViewModel());
            }
            else
            {
                return (EResultCode.Error, new ChatViewModel());
            }
        }

        public async Task<(EResultCode, ChatViewModel)> CreateGroupChat(string Name, List<int> UsersId)
        {
            var result = CreateGroupChatValidation(Name, UsersId);

            var httpContext = _httpContextAccessor.HttpContext;
            var Users = _UserRepository.GetAsync().Result;
            if (httpContext == null)
                return (EResultCode.DbError, null);

            var MainId = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
            var GroupUsers = new List<User>();
            foreach(var UserId in UsersId)
            { 
                GroupUsers.Add(Users.First(u => u.Id == UserId));
            }
            Chat GroupChat = new Chat
            {
                Name = Name,
                Admin = Users[MainId],
                ChatType = EChatType.Group,
                CreatedAt = DateTime.Now,
                Users = GroupUsers
            };
            await _ChatRepository.AddAsync(GroupChat);
 
            return (EResultCode.Success, ChatDTO.ChatToViewModel(GroupChat));
        }


        public async Task<(EResultCode, List<ChatViewModel>)> GetChats()
        {

            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return (EResultCode.DbError, null);

            var user = _UserRepository.GetByIdAsync(Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value));

            var chats = user.Result.Chats;
            
            return (EResultCode.Success, ChatDTO.ChatsToViewModel(chats));
        }


        public EResultCode CreatePrivateChatValidation(int Id)
        { 

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return EResultCode.DbError;
            var MainId = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
            var Users = _UserRepository.GetAsync().Result;
            var Chats = _ChatRepository.GetAsync().Result;
            if (Users[Id] != null) 
                return EResultCode.NotExist; 
            if (MainId != Id)
                return EResultCode.Error;

            if(false)//haveNotPermission
                return EResultCode.HasNotPermission;
            if(Chats.Any(c => c.Users.Contains(Users[MainId]) && c.Users.Contains(Users[Id])))
                return EResultCode.ThisRoomAlreadyExist;
            return EResultCode.Success;
        }

        public EResultCode CreateGroupChatValidation(string Name, List<int> UsersId) 
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return EResultCode.SomeFieldsEmpty;
            }
            var users = _UserRepository.GetAsync().Result;

            var existedUsers = users.All(u => UsersId.Contains(u.Id));
            if (!existedUsers)
                return EResultCode.NotExist;

            if (UsersId.Count < MinCountGroupMembers)
                return EResultCode.Error;

            return EResultCode.Success;
        }
    }
} 