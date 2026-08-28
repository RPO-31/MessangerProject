using Messanger.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messanger.DataAccess.Models
{
    public class Chat
    { 
        public int Id { get; set; }

        public EChatType ChatType { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public int? AdminId { get; set; }
        public User Admin { get; set; } = null;

        public List<int> MessagesId { get; set; } = new();
        public List<Message> Messages { get; set; } = new();

        public List<int> UsersId { get; set; } = new();
        public List<User> Users { get; set; } = new();

    }
}