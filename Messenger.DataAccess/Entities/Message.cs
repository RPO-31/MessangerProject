using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DataAccess.Classes
{
    public class Message
    {
        public int Id { get; set; }

        public Chat Chat { get; set; }

        public int AuthorId { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public bool isDeleted { get; set; } = false;

    }
}
