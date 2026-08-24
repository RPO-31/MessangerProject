using Mesenger.Api.DTO.RequestClasses;
using Messanger.Api.Enums;

namespace Messanger.Api.Services.Interfaces
{
    public interface IRegisterService
    { 
        Task<Result> RegValidation(RegisterRequest RegRequest);  
    }
}
