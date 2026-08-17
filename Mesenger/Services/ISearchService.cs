using Messenger.DataAccess.Classes;

namespace Mesenger.Api.Services
{
    public interface ISearchService
    { 
        Task<List<User>> SearchUsers(string OutputName);
    }
}
