using Messanger.Frontend.Enums;
using Microsoft.AspNetCore.Identity.Data;

namespace Messanger.Frontend.Services
{
    public interface IRegisterService
    {
        public Task<CodeTypes> RegValitation(RegisterRequest RegRequest);
      
    }
}
