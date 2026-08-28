using Messanger.DataAccess.Enums;
using Messanger.DataAccess.Models;

namespace Messanger.Api.ViewModels
{
    public class ChatViewModel
    {
        public int Id { get; set; }

        public string ChatType { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Name { get; set; } = string.Empty;

        public string LastMessage { get; set; }

        public List<string> Users { get; set; }
    }
}
