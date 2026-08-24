using Mesenger.Api.DTO.RequestClasses;
using Messanger.Api.Enums;

namespace Messanger.Api.Services.Interfaces
{
    public interface ILoginService
    {
        Task<Result> LogValidation(LoginRequest logRequest);
        Task<Result> LogOut();
    }
}
