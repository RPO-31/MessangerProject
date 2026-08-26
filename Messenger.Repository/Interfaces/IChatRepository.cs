using Messanger.DataAccess.Models;

namespace Messenger.Repository.Interfaces
{
    public interface IChatRepository
    {
        public Task<List<Chat>> GetAsync();

        public Task<Chat> GetByIdAsync(int id);

        public Task AddAsync(Chat chat);

        public Task SaveChangesAsync();
    }
}
