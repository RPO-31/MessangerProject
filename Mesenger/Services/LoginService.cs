using Mesenger.Api.DTO.RequestClasses;
using Messanger.Api.Enums;
using Messanger.Api.Services.Interfaces;
using Messanger.DataAccess.Models;
using Messenger.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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

        public async Task<Result> LogValidation(LoginRequest logRequest)
        { 
            
            if(logRequest == null)
                return new Result(EResultCode.NotFound, "нету данных!");
            
            var Users = await _UserRepository.GetAsync(); 
            var user = Users.Where(u => string.Equals(u.Name,logRequest.NameOrEmail) || string.Equals(u.Email, logRequest.NameOrEmail)).FirstOrDefault();
             
            if( user == null)
            {
                return new Result(EResultCode.NotExist, "данного пользователя не существует");
            }
            //var passwordHasher = new PasswordHasher<User>();

            //var result = passwordHasher.VerifyHashedPassword(user, user.Password, logRequest.Password); 
            
            //if(result == PasswordVerificationResult.Failed)
                //return new Result(EResultCode.Invalid_Field, "неверный пароль");

            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return new Result(EResultCode.DbError, "");

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            });
            //if (result == PasswordVerificationResult.Success)
                return new Result(EResultCode.Success, "успешно");
            //else
            //    return new Result(EResultCode.Invalid_Field, "успешно, однако нужен новый хэш для пароля!");
        }

        public async Task<Result> LogOut()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return new Result(EResultCode.DbError, "");

            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return new Result(EResultCode.Success, "Успешно");
        }
    }
} 