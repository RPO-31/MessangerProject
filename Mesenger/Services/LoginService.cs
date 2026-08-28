using Mesenger.Api.DTO.RequestClasses;
using Messanger.Api.Enums;
using Messanger.Api.Services.Interfaces;
using Messanger.DataAccess.Models;
using Messenger.Api.Repository.Interfaces; 
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace Messanger.Api.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _UserRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _UserRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result> LogValidation(LoginRequestDTO logRequest)
        {

            if (logRequest == null)
                return new Result(EResultCodes.NotFound, "нету данных!");

            var Users = await _UserRepository.GetAsync();
            var user = Users.Where(u => string.Equals(u.Name, logRequest.NameOrEmail) || string.Equals(u.Email, logRequest.NameOrEmail)).FirstOrDefault();

            if (user == null)
            {
                return new Result(EResultCodes.NotExist, "Данного пользователя не существует!");
            }

            //var passwordHasher = new PasswordHasher<User>();
            //var result = passwordHasher.VerifyHashedPassword(user, user.Password, logRequest.Password);
            //
            //if (result == PasswordVerificationResult.Failed)
            //    return new Result(EResultCodes.Invalid_Field, "Неверный пароль!");
            //
            //else if (result == PasswordVerificationResult.SuccessRehashNeeded)
            //{
            //    user.Password = passwordHasher.HashPassword(user, logRequest.Password);
            //}

            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return new Result(EResultCodes.DbError, "");

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            });
            return new Result(EResultCodes.Success, "успешно");
        }

        public async Task<Result> LogOut()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return new Result(EResultCodes.DbError, "");

            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return new Result(EResultCodes.Success, "Успешно");
        }
    }
}