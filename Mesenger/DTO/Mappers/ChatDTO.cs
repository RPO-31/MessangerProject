using Messanger.Api.ViewModels;
using Messanger.DataAccess.Enums;
using Messanger.DataAccess.Models;

namespace Mesenger.Api.DTO.Transformers
{
    public static class ChatDTO
    {
        public static ChatViewModel ChatToViewModel(Chat chat)
        {
            List<string> users = new();
            foreach (var user in chat.Users)
                users.Add(user.Name + " ");
            return new ChatViewModel()
            {
                Id = chat.Id,
                ChatType = chat.ChatType == EChatType.Personal ? "Приватный" : "Групповой",
                Name = chat.ChatType == EChatType.Personal ? users[0] : chat.Name,
                CreatedAt = chat.CreatedAt,
                LastMessage = chat.Messages.Count > 0 ? chat.Messages.LastOrDefault().Text : "",
                Users = users
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