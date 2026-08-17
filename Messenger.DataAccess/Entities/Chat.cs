using Mesenger.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DataAccess.Classes
{
    public class Chat
    {
        public int Id { get; set; }

        public EChatType ChatType;

        public string Name { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public User ChatAdmin { get; set; } = null;
    }
}
