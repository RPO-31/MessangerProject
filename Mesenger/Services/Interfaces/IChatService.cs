using Messanger.Api.Enums;
using Messanger.Api.ViewModels;
using Messanger.DataAccess.Models;
using Messenger.Repository.Interfaces;

namespace Mesenger.Api.Services.Interfaces
{
    public interface IChatService
    {  
        Task<(EResultCode, ChatViewModel)> CreatePrivateChat(int Id);
        Task<(EResultCode, ChatViewModel)> CreateGroupChat(string Name, List<int> UsersId);

        Task<(EResultCode, List<ChatViewModel>)> GetChats(); 

        EResultCode CreateGroupChatValidation(string Name, List<int> UsersId);
        EResultCode CreatePrivateChatValidation(int Id);

    }
} 