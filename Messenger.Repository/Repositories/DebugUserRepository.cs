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
    public class DebugUserRepository : IUserRepository
    {

        public static List<User> Users2 = new List<User>(){
                //new User{Id = 1, Email = "RomeoJudge@gmail.com", Name = "Ники", OutputName = "Ники write", Password = "Z9y$KlmN", RegDate = DateTime.Now},
                //new User{Id = 2, Email = "Alfedov@gmail.com", Name = "Альфедов", OutputName = "Альфедов yt", Password = "Z9y$KlmN", RegDate = DateTime.Now},
                new User{Id = 0, Email = "JustS@gmail.com", Name = "Джаст", OutputName = "Джаст orig", Password = "Z9y$KlmN", RegDate = DateTime.Now, Chats = new List<Chat>()
                    {
                        new Chat(){ Id = 0,ChatType = EChatType.Personal, CreatedAt = DateTime.Now, Messages = new List<Message>(){ new Message(){Text = "что там по peak"}, new Message(){Text = "дважды", Author = new User{Id = 0, Email = "JustS@gmail.com", Name = "Джаст", OutputName = "Джаст orig", Password = "Z9y$KlmN", RegDate = DateTime.Now } } }, Users = new(){new User{Id = 0, Email = "JustS@gmail.com", Name = "Джаст", OutputName = "Джаст orig", Password = "Z9y$KlmN", RegDate = DateTime.Now } } },
                        new Chat(){ Id = 1, Name = "лол чат для четверых(4)",  ChatType = EChatType.Group, CreatedAt = DateTime.Now, Messages = new List<Message>(){ new Message(){Text = "что там по peak2"}, new Message(){Text = "трижды"}  }, Users = new(){new User{Id = 0, Email = "JustS@gmail.com", Name = "Джаст", OutputName = "Джаст orig", Password = "Z9y$KlmN", RegDate = DateTime.Now } } },
                    }
                },
                new User{Id = 1, Email = "JustS@gmail.com", Name = "Джаст", OutputName = "Джаст orig", Password = "Z9y$KlmN", RegDate = DateTime.Now, Chats = new List<Chat>()
                    {
                        new Chat(){ Id = 0, ChatType = EChatType.Personal, CreatedAt = DateTime.Now, Messages = new List<Message>(){ new Message(){Text = "что там по peak"}, new Message(){Text = "дважды"} }, Users = new(){new User{Id = 0, Email = "JustS@gmail.com", Name = "Джаст", OutputName = "Джаст orig", Password = "Z9y$KlmN", RegDate = DateTime.Now } } },
                        new Chat(){ Id = 1, ChatType = EChatType.Group, CreatedAt = DateTime.Now, Messages = new List<Message>(){ new Message(){Text = "что там по peak2"}, new Message(){Text = "трижды"}  }, Users = new(){new User{Id = 0, Email = "JustS@gmail.com", Name = "Джаст", OutputName = "Джаст orig", Password = "Z9y$KlmN", RegDate = DateTime.Now } } },
                    }
                },
                new User{Id = 2, Email = "JustS@gmail.com", Name = "Джаст", OutputName = "Джаст orig2", Password = "Z9y$KlmN", RegDate = DateTime.Now, Chats = new List<Chat>()
                    {
                        new Chat(){ Id = 0, ChatType = EChatType.Personal, CreatedAt = DateTime.Now, Messages = new List<Message>(){ new Message(){Text = "что там по peak"}, new Message(){Text = "дважды"} }, Users = new(){new User{Id = 0, Email = "JustS@gmail.com", Name = "Джаст", OutputName = "Джаст orig", Password = "Z9y$KlmN", RegDate = DateTime.Now } } },
                        new Chat(){ Id = 1, ChatType = EChatType.Group, CreatedAt = DateTime.Now, Messages = new List<Message>(){ new Message(){Text = "что там по peak2"}, new Message(){Text = "трижды"}  }, Users = new(){new User{Id = 0, Email = "JustS@gmail.com", Name = "Джаст", OutputName = "Джаст orig", Password = "Z9y$KlmN", RegDate = DateTime.Now } } },
                    }
                }, 
                //new User{Id = 4, Email = "Alcest@gmail.com", Name = "Альцест", OutputName = "Альцест raketa", Password = "Z9y$KlmN", RegDate = DateTime.Now},
                //?new User{Id = 5, Email = "SirPiligrim@gmail.com", Name = "СирПилигрим", OutputName = "СирПилигрим sirpilya", Password = "Z9y$KlmN", RegDate = DateTime.Now},
        };
        public async Task<List<User>> GetAsync()
        {
            return Users2;
        }

        public async Task<User> GetByIdAsync(int id) 
        {
            return Users2[id];
        }

        public async Task AddAsync(User user) 
        {
            Users2.Add(user);
        }

        public async Task SaveChangesAsync() 
        {
            
        }
    }
} 