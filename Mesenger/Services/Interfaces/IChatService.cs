using Mesenger.Api.DTO.RequestClasses;
using Messanger.Api.Enums;
using Messanger.Api.ViewModels;

namespace Mesenger.Api.Services.Interfaces
{
    public interface IChatService
    {
        Task<Result> CreatePrivateChat(PrivateChatRequestDTO PrivateRequest);
        Task<Result> CreateGroupChat(GroupChatRequestDTO GroupRequest);

        Task<(Result, List<ChatViewModel>)> GetChats();
        Task<(Result, ChatViewModel)> GetChatById(int Id);

        Task<Result> CreateGroupChatValidation(GroupChatRequestDTO GroupRequest);
        Task<Result> CreatePrivateChatValidation(PrivateChatRequestDTO PrivateRequest);

        Task<(Result, List<MessageViewModel>)> GetChatMessages(int Id);

        Task<Result> SendChatMessages(int Id, SendMsgRequestDTO SendMsgRequest);
    }
}