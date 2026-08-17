

using Messenger.DataAccess.Classes;
using Messenger.Repository;

namespace Mesenger.Api.Services
{
    public class SearchService
    {
        private readonly IUserRepository _UserRepository;
        public async Task<List<User>> SearchUsers(string OutputName) 
        {
            var users = _UserRepository.GetUsers().Result;

            var result = users.Where(u => Search(u.OutputName, OutputName)).ToList();
            if(result.Count > 0)//()
                return result;
            else
                return 
        }
        public bool Search(string Name, string OtherName)
        {
            if (Name.Equals(OtherName, StringComparison.OrdinalIgnoreCase))
                return true;
            else
                return false;
        }

    }
}
