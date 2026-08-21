using Messanger.Api.ViewModels;
using Messanger.DataAccess.Models;

namespace Mesenger.Api.DTO.Transformers
{
    public static class ChatDTO
    {
        public static ChatViewModel ChatToViewModel(Chat chat)
        {
            return new ChatViewModel()
            {
                Id = chat.Id,
                ChatType = chat.ChatType,
                Name = chat.Name,
                CreatedAt = chat.CreatedAt,
                //Messages = chat.Messages,
                Users = chat.Users
            };
        }    
        public static List<ChatViewModel> ChatsToViewModel(List<Chat> chats)
        {
            var result = new List<ChatViewModel>();

            foreach (var chat in chats)
            {
                result.Add(ChatToViewModel(chat));
            }
            return result;
        }
    } 
} 