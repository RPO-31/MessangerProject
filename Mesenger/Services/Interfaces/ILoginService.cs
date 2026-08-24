using Mesenger.Api.DTO.RequestClasses;
using Messanger.Api.Enums;

namespace Messanger.Api.Services.Interfaces
{
    public interface ILoginService
    {
        Task<Result> LogValidation(LoginRequestDTO logRequest);
        Task<Result> LogOut();
    }
}
