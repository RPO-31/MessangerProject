using Messenger.DataAccess.Classes;

namespace Messenger.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
    }
}
