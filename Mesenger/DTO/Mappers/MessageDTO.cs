using Messanger.Api.ViewModels;
using Messanger.DataAccess.Models;

namespace Mesenger.Api.DTO.Transformers
{
    public class MessageDTO
    {
        public static MessageViewModel MessageToViewModel(Message message)
        {
            return new MessageViewModel()
            {
                Text = message.Text,
                CreatedAt = message.CreatedAt,
                AuthorName = (message.Author == null) ? message.Author.OutputName : ""
            };
        }
        public static List<MessageViewModel> MessagesToViewModel(List<Message> messages)
        {
            var result = new List<MessageViewModel>();
            foreach(var message in messages)
            {
                result.Add(MessageToViewModel(message));
            }
            return result;
        }
    }
} 