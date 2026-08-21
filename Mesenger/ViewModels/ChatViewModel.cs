using Messanger.DataAccess.Enums;
using Messanger.DataAccess.Models;

namespace Messanger.Api.ViewModels
{
    public class ChatViewModel
    {
        public int Id { get; set; }

        public EChatType ChatType { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Name { get; set; }

        public List<Message> Messages { get; set; }

        public List<User> Users { get; set; }
    }
}
