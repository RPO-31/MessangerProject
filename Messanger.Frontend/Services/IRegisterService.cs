using Messanger.Frontend.Enums;

namespace Messanger.Frontend.Services
{
    public interface IRegisterService
    {
        public Task<CodeTypes> RegValitation((string login, string name, string email, string password, string passwordrepeat);
      
    }
}
