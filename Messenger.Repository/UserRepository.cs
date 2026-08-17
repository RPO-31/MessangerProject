using Messenger.DataAccess.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<List<User>> GetUsers()
        {
            return new List<User>();
        }
    }
}
