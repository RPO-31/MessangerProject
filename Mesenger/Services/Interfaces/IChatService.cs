using Mesenger.Api.DTO.RequestClasses;
using Messanger.Api.Enums;
using Messanger.Api.ViewModels;
using Messanger.DataAccess.Models;
using Messenger.Repository.Interfaces;

namespace Mesenger.Api.Services.Interfaces
{
    public interface IChatService
    {  
        Task<(Result, ChatViewModel)> CreatePrivateChat(PrivateChatRequest PrivateRequest);
        Task<(Result, ChatViewModel)> CreateGroupChat(GroupChatRequest GroupRequest);

        Task<(Result, List<ChatViewModel>)> GetChats(); 
        Task<(Result, ChatViewModel)> GetChatById(int Id);

        EResultCode CreateGroupChatValidation(GroupChatRequest GroupRequest);
        (EResultCode, ChatViewModel) CreatePrivateChatValidation(PrivateChatRequest PrivateRequest);

    }
} 