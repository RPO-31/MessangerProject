using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Api.DataAccess.Entities
{
    public class ChatMember
    {
        public int ChatId { get; set; }


        public int UserId { get; set; }

        public DateTime JoinedAt { get; set; }
    }
}
