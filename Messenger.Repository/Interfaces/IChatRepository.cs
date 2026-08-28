using Messanger.DataAccess.Models;

namespace Messenger.Api.Repository.Interfaces
{
    public interface IChatRepository
    {
        Task<List<Chat>> GetAsync();

        Task<Chat> GetByIdAsync(int id);

        Task AddAsync(Chat chat);

        Task SaveChangesAsync();
    }
}