using Messanger.DataAccess.Models;
using Messenger.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Messenger.Api.Repository.Repositories
{
    public class EFChatRepository : IChatRepository
    {
        private readonly AppDbContext _Db;

        public EFChatRepository(AppDbContext Db)
        {
            _Db = Db;
        }
        public async Task<List<Chat>> GetAsync()
        {
            return await _Db.Chats.ToListAsync();
        }

        public async Task<Chat> GetByIdAsync(int id)
        {
            return await _Db.Chats.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddAsync(Chat chat)
        {
            await _Db.Chats.AddAsync(chat);
        }

        public async Task SaveChangesAsync()
        {
            await _Db.SaveChangesAsync();
        }
    }
}
