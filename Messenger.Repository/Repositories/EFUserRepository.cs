using Messanger.DataAccess.Models;
using Messenger.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Api.Repository.Repositories
{
    public class EFUserRepository : IUserRepository
    {
        private readonly AppDbContext _Db;

        public EFUserRepository(AppDbContext Db)
        {
            _Db = Db;
        }
        public async Task<List<User>> GetAsync()
        {
            return await _Db.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _Db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddAsync(User user)
        {
            await _Db.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _Db.SaveChangesAsync();
        }
    }
}
//dotnet ef migrations add InitialCreate
