using Mesenger.Api.DTO.RequestClasses;
using Mesenger.Api.DTO.Transformers;
using Mesenger.Api.Services.Interfaces;
using Messanger.Api.Enums;
using Messanger.Api.ViewModels;
using Messanger.DataAccess.Enums;
using Messanger.DataAccess.Models;
using Messenger.Api.Repository.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Mesenger.Api.Services
{
    public class ChatService : IChatService
    {
        private readonly IUserRepository _UserRepository;
        private readonly IChatRepository _ChatRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const int MinCountGroupMembers = 3;
        private const int MaxLengthOfMsg = 50;

        public ChatService(IUserRepository UserRepository, IChatRepository ChatRepository, IHttpContextAccessor httpContextAccessor)
        {
            _UserRepository = UserRepository;
            _ChatRepository = ChatRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result> CreatePrivateChat(PrivateChatRequestDTO PrivateRequest)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return new Result(EResultCodes.Error, "Ошибка");

            var idStr = httpContext.User.FindFirst("Id")?.Value;
            int MainId = int.TryParse(idStr, out int parsedId) ? parsedId : -1;

            if (MainId == -1)
                return new Result(EResultCodes.Unauthorized, "Не авторизован");

            var ResultCode = await CreatePrivateChatValidation(PrivateRequest);

            if (ResultCode.SResultCode == EResultCodes.Success)
            {
                var usersToChat = new List<User>();
                var usersIdToChat = new List<int>();

                var MainUser = await _UserRepository.GetByIdAsync(MainId);
                var OtherUser = await _UserRepository.GetByIdAsync(PrivateRequest.UserId);

                usersToChat.Add(MainUser);
                usersToChat.Add(OtherUser);

                usersIdToChat = usersToChat.Select(u => u.Id).ToList();

                var chat = new Chat() { ChatType = EChatType.Personal, CreatedAt = DateTime.Now, Users = usersToChat, UsersId = usersIdToChat };

                MainUser.Chats.Add(chat);
                OtherUser.Chats.Add(chat);

                await _ChatRepository.AddAsync(chat);

                return new Result(EResultCodes.Success, "Успешно");
            }
            else if (ResultCode.SResultCode == EResultCodes.ThisRoomAlreadyExist)
            {
                return new Result(EResultCodes.ThisRoomAlreadyExist, $"chatExist {ResultCode.SMessage}");
            }
            else
            {
                return new Result(EResultCodes.Error, "Неизвестная ошибка");
            }
        }

        public async Task<Result> CreateGroupChat(GroupChatRequestDTO GroupRequest)
        {
            var result = CreateGroupChatValidation(GroupRequest);
            var httpContext = _httpContextAccessor.HttpContext;
            var Users = await _UserRepository.GetAsync();

            if (httpContext == null)
                return new Result(EResultCodes.Error, "Ошибка");

            var idStr = httpContext.User.FindFirst("Id")?.Value;
            int MainId = int.TryParse(idStr, out int parsedId) ? parsedId : -1;

            if (MainId == -1)
                return new Result(EResultCodes.Unauthorized, "Не авторизован");

            var GroupUsers = new List<User>();
            GroupUsers.Add(Users[MainId]);
            var GroupUsersId = new List<int>();
            GroupUsersId.Add(Users[MainId].Id);

            foreach (var UserId in GroupRequest.UsersId)
            {
                var tempUser = Users.First(u => u.Id == UserId);
                GroupUsers.Add(tempUser);
                GroupUsersId.Add(tempUser.Id);
            }

            Chat GroupChat = new Chat
            {
                Name = GroupRequest.Name,
                Admin = Users[MainId],
                ChatType = EChatType.Group,
                CreatedAt = DateTime.Now,
                Users = GroupUsers,
                UsersId = GroupUsersId
            };
            for (int i = 0; i < GroupChat.Users.Count; i++)
            {
                GroupChat.Users[i].Chats.Add(GroupChat);
            }

            await _ChatRepository.AddAsync(GroupChat);
            return new Result(EResultCodes.Success, "Успешно");
        }

        public async Task<(Result, List<ChatViewModel>)> GetChats()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return (new Result(EResultCodes.Error, "Ошибка"), default!);

            var idStr = httpContext.User.FindFirst("Id")?.Value;
            int MainId = int.TryParse(idStr, out int parsedId) ? parsedId : -1;

            if (MainId == -1)
                return (new Result(EResultCodes.Unauthorized, "Не авторизован"), default!);

            var user = await _UserRepository.GetByIdAsync(MainId);

            var chats = user.Chats;
            if (chats.Count == 0)
                return (new Result(EResultCodes.SomeFieldsEmpty, "На данный момент у вас нету ни одной активной беседы"), default!);

            return (new Result(EResultCodes.Success, "Успешно"), ChatDTO.ChatsToViewModel(chats));
        }

        public async Task<(Result, ChatViewModel)> GetChatById(int Id)
        {

            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return (new Result(EResultCodes.Error, "Ошибка!"), default!);

            var idStr = httpContext.User.FindFirst("Id")?.Value;
            int MainId = int.TryParse(idStr, out int parsedId) ? parsedId : -1;

            if (MainId == -1)
                return (new Result(EResultCodes.Unauthorized, "Не авторизован!"), default!);

            var user = await _UserRepository.GetByIdAsync(MainId);

            var chat = user.Chats.FirstOrDefault(c => c.Id == Id);
            if (chat == null)
                return (new Result(EResultCodes.NotFound, "Данный чат не найден!"), default!);

            return (new Result(EResultCodes.Success, "Успешно!"), ChatDTO.ChatToViewModel(chat));
        }


        public async Task<Result> CreatePrivateChatValidation(PrivateChatRequestDTO PrivateRequest)
        {

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return new Result(EResultCodes.Error, "Ошибка");

            var idStr = httpContext.User.FindFirst("Id")?.Value;
            int MainId = int.TryParse(idStr, out int parsedId) ? parsedId : -1;

            if (MainId == -1)
                return new Result(EResultCodes.Unauthorized, "Не авторизован!");

            var Users = await _UserRepository.GetAsync();
            var Chats = await _ChatRepository.GetAsync();

            if (Users[PrivateRequest.UserId] == null)
                return new Result(EResultCodes.NotExist, "Данного пользователя не существует!");

            if (MainId == PrivateRequest.UserId)
                return new Result(EResultCodes.Error, "Вы не можете создать беседу с самим собой!(извините)");


            var chatWhatExist = Chats.Where(c => c.Users.Contains(Users[MainId]) && c.Users.Contains(Users[PrivateRequest.UserId])).FirstOrDefault();

            if (chatWhatExist != null)
                return new Result(EResultCodes.ThisRoomAlreadyExist, chatWhatExist.Id.ToString());
            return new Result(EResultCodes.Success, "Успешно!");
        }

        public async Task<Result> CreateGroupChatValidation(GroupChatRequestDTO GroupRequest)
        {

            if (string.IsNullOrWhiteSpace(GroupRequest.Name))
                return new Result(EResultCodes.SomeFieldsEmpty, "Напишите название для групповойй беседы!");

            var users = await _UserRepository.GetAsync();

            var existedUsers = users.All(u => GroupRequest.UsersId.Contains(u.Id));

            if (!existedUsers)
                return new Result(EResultCodes.NotExist, "Одного/Нескольких пользователей Не существует!");

            if (GroupRequest.UsersId.Count < MinCountGroupMembers)
                return new Result(EResultCodes.Error, $"необходимо выбрать минимум {MinCountGroupMembers} пользователей!");

            return new Result(EResultCodes.Success, "Успешно!");
        }


        public async Task<(Result, List<MessageViewModel>)> GetChatMessages(int Id)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return (new Result(EResultCodes.Error, "Ошибка!"), null!);

            var idStr = httpContext.User.FindFirst("Id")?.Value;
            int MainId = int.TryParse(idStr, out int parsedId) ? parsedId : -1;

            if (MainId == -1)
                return (new Result(EResultCodes.Unauthorized, "Не авторизован!"), default!);

            var chat = await _ChatRepository.GetByIdAsync(Id);
            var user = await _UserRepository.GetByIdAsync(MainId);

            if (chat.Users.Contains(user))
            {
                return (new Result(EResultCodes.NotFound, "Вы не состоите в данной беседе!"), null!);
            }
            var messages = (chat.Messages.Count == 0) ? (new List<MessageViewModel>()) : MessageDTO.MessagesToViewModel(chat.Messages);

            return (new Result(EResultCodes.Success, "Успешно"), messages);
        }

        public async Task<Result> SendChatMessages(int Id, SendMsgRequestDTO SendMsgRequest)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return new Result(EResultCodes.Error, "Ошибка!");

            var idStr = httpContext.User.FindFirst("Id")?.Value;
            int MainId = int.TryParse(idStr, out int parsedId) ? parsedId : -1;

            if (MainId == -1)
                return new Result(EResultCodes.Unauthorized, "Не авторизован!");

            var chat = await _ChatRepository.GetByIdAsync(Id);

            if (chat == null)
                return new Result(EResultCodes.Error, "данного чата не существует!");

            if (!chat.Users.Any(u => u.Id == MainId))
                return new Result(EResultCodes.Error, "Вы не являетесь участником чата!");

            if (string.IsNullOrWhiteSpace(SendMsgRequest.Text))
                return new Result(EResultCodes.SomeFieldsEmpty, "текст пустой");

            if (SendMsgRequest.Text.Length > MaxLengthOfMsg)
                return new Result(EResultCodes.OutOfLimits, "Превышен лимит по сообщению");


            var user = await _UserRepository.GetByIdAsync(MainId);

            chat.Messages.Add(new Message()
            {
                Text = SendMsgRequest.Text,
                CreatedAt = DateTime.Now,
                Author = user,
                MainChat = chat,
                MainChatId = chat.Id
            });
            await _ChatRepository.SaveChangesAsync();

            return new Result(EResultCodes.Success, "Успешно!");
        }
    }
}