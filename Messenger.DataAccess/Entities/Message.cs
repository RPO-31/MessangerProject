using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messanger.DataAccess.Models
{
    public class Message
    {
        public int Id { get; set; }

        public int MainChatId { get; set; }
        public Chat? MainChat { get; set; } = new();

        public User? Author { get; set; } = new();

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; } = false; 

    }
}
