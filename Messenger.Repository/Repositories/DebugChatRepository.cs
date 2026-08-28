using Messanger.DataAccess.Enums;
using Messanger.DataAccess.Models;
using Messenger.Api.Repository.Interfaces; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Api.Repository.Repositories
{
    public class DebugChatRepository : IChatRepository
    {
        public static List<Chat> Chats2 = DebugUserRepository.Users2[0].Chats; 
       
            
            //new Chat(){ Id = 1, ChatType = EChatType.Personal, CreatedAt = DateTime.Now, Messages = new List<Message>(){ new Message(){Text = "что там по peak"}, new Message(){Text = "дважды"} }, Users = new(){UserRepository.Users2[0]}},
            //new Chat(){ Id = 0, ChatType = EChatType.Group, CreatedAt = DateTime.Now, Messages = new List<Message>(){ new Message(){Text = "что там по peak2"}, new Message(){Text = "трижды"}  }}// List<.

       
        public async Task<List<Chat>> GetAsync()
        {
            return Chats2;
        }

        public async Task<Chat> GetByIdAsync(int id)
        {

            return Chats2[id];
        }
        public Task SetMsgByIdAsync(int id, Message msg)
        { 
            Chats2[id].Messages.Add(msg);
            return Task.CompletedTask; 
        }

        public async Task AddAsync(Chat chat)
        {
            Chats2.Add(chat);
        }

        public async Task SaveChangesAsync()
        {

        }
    }
}
