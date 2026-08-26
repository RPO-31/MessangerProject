using Mesenger.Api.DTO.RequestClasses;
using Messanger.Api.Enums;
using Messanger.Api.ViewModels;

namespace Mesenger.Api.Services.Interfaces
{
    public interface IChatService
    {  
        Task<(Result, ChatViewModel)> CreatePrivateChat(PrivateChatRequestDTO PrivateRequest);
        Task<(Result, ChatViewModel)> CreateGroupChat(GroupChatRequestDTO GroupRequest);

        Task<(Result, List<ChatViewModel>)> GetChats();
        Task<(Result, ChatViewModel)> GetChatById(int Id);

        Task<Result> CreateGroupChatValidation(GroupChatRequestDTO GroupRequest);
        Task<(Result, ChatViewModel)> CreatePrivateChatValidation(PrivateChatRequestDTO PrivateRequest);

    }
} 