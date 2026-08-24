using Messanger.DataAccess.Enums;
using Messanger.DataAccess.Models;

namespace Messanger.Api.ViewModels
{
    public class ChatViewModel
    {
        public int Id { get; set; }

        public string ChatType { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Name { get; set; }

        public List<MessageViewModel> Messages { get; set; }

        public List<UserViewModel> Users { get; set; }
    }
}
