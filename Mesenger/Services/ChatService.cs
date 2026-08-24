using Mesenger.Api.DTO.RequestClasses;
using Mesenger.Api.DTO.Transformers;
using Mesenger.Api.Services.Interfaces;
using Messanger.Api.Enums;
using Messanger.Api.ViewModels;
using Messanger.DataAccess.Enums;
using Messanger.DataAccess.Models;
using Messenger.Repository.Interfaces; 

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
        public async Task<(Result, ChatViewModel)> CreatePrivateChat(PrivateChatRequestDTO PrivateRequest)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return (new Result(EResultCode.DbError, ""), null!);

            var MainId = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
            var ResultCode = await CreatePrivateChatValidation(PrivateRequest);

            if(ResultCode.Item1.SResultCode == EResultCode.Success)
            {
                var usersToChat = new List<User>();
                usersToChat.Add(await _UserRepository.GetByIdAsync(MainId));
                usersToChat.Add(await _UserRepository.GetByIdAsync(PrivateRequest.UserId));
                var chat = new Chat() { ChatType = EChatType.Personal, CreatedAt = DateTime.Now, Users = usersToChat };
                var MainUser = await _UserRepository.GetByIdAsync(MainId);
                MainUser.Chats.Add(chat);
                
                var user = await _UserRepository.GetByIdAsync(PrivateRequest.UserId);
                user.Chats.Add(chat);

                await _ChatRepository.AddAsync(chat);
                return (new Result(EResultCode.Success, "Успешно"), ChatDTO.ChatToViewModel(chat)); 
            }
            else if(ResultCode.Item1.SResultCode == EResultCode.ThisRoomAlreadyExist)
            {
                return (new Result(EResultCode.ThisRoomAlreadyExist, "Комната уже существует"), ResultCode.Item2);
            }
            else
            {
                return (new Result(EResultCode.Error, "Неизвестная ошибка"), new ChatViewModel());
            }
        }

        public async Task<(Result, ChatViewModel)> CreateGroupChat(GroupChatRequestDTO GroupRequest)
        {
            var result = CreateGroupChatValidation(GroupRequest);

            var httpContext = _httpContextAccessor.HttpContext;
            var Users = await _UserRepository.GetAsync();
            if (httpContext == null)
                return (new Result(EResultCode.DbError, "не авторизован!"), default!);

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
                return (new Result(EResultCode.DbError, "не авторизован!"), null);

            var user = await _UserRepository.GetByIdAsync(Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value));

            var chats = user.Chats;
            
            return (new Result(EResultCode.Success, "Успешно"), ChatDTO.ChatsToViewModel(chats));
        }

        public async Task<(Result, ChatViewModel)> GetChatById(int Id)
        {

            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return (new Result(EResultCode.DbError, "дбдэррбро"), null);

            var user = await _UserRepository.GetByIdAsync(Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value));

            var chat = user.Chats.First(c => c.Id == Id); 

            return (new Result(EResultCode.Success, "Успешно"), ChatDTO.ChatToViewModel(chat));
        }

        public async Task<(Result, ChatViewModel)> CreatePrivateChatValidation(PrivateChatRequestDTO PrivateRequest)
        { 

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return (new Result(EResultCode.DbError, ""), null!);
            var MainId = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
            var Users = await _UserRepository.GetAsync();
            var Chats = await _ChatRepository.GetAsync();
            if (Users[PrivateRequest.UserId] != null) 
                return (new Result(EResultCode.NotExist, ""), null!); 
            if (MainId != PrivateRequest.UserId)
                return (new Result(EResultCode.Error, ""), null!);

            if(false)//haveNotPermission
                return (new Result(EResultCode.HasNotPermission, ""), new ChatViewModel());
            var IsExist = Chats.Where(c => c.Users.Contains(Users[MainId]) && c.Users.Contains(Users[PrivateRequest.UserId])).FirstOrDefault();
            if(IsExist != null)
                return (new Result(EResultCode.ThisRoomAlreadyExist, ""), ChatDTO.ChatToViewModel(IsExist));
            return (new Result(EResultCode.Success, ""), null!);
        }

        public async Task<Result> CreateGroupChatValidation(GroupChatRequestDTO GroupRequest) 
        {

            if (string.IsNullOrWhiteSpace(GroupRequest.Name))
            {
                return new Result(EResultCode.SomeFieldsEmpty, "");
            }
            var users = await _UserRepository.GetAsync();

            var existedUsers = users.All(u => GroupRequest.UsersId.Contains(u.Id));
            if (!existedUsers)
                return new Result(EResultCode.NotExist, "");

            if (GroupRequest.UsersId.Count < MinCountGroupMembers)
                return new Result(EResultCode.Error, "");

            return new Result(EResultCode.Success, "");
        }
    }
} 