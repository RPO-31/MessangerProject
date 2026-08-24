using Messanger.Api.Enums;
using Messanger.DataAccess.Models;
using Messenger.Repository.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using Messanger.Api.Services.Interfaces; 
using Mesenger.Api.DTO.RequestClasses;

namespace Messanger.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterService _RegisterService;
        private readonly ILoginService _LoginService;
        private readonly ISearchUsersService _SearchUsersService; 

        public AuthController(IRegisterService registerService, ILoginService LoginService, ISearchUsersService SearchUsersService)
        {
            _RegisterService = registerService;
            _LoginService = LoginService;
           _SearchUsersService = SearchUsersService;
        }
        
        [Authorize]
        [HttpGet("Users")]
        public List<User> GetUsers()
        {
            return UserRepository.Users2;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegValid([FromBody] RegisterRequest RegRequest)
        {
            var result = _RegisterService.RegValidation(RegRequest).Result;

            if (result.SResultCode == EResultCode.Success)
                return Created();
            else
                return BadRequest( new { message = result.SMessage } );
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult LogValidation([FromBody] LoginRequest loginRequest)
        {
            var result = _LoginService.LogValidation(loginRequest).Result;
            if (result.SResultCode == EResultCode.Success)
                return Created();
            else
                return BadRequest(new { message = result.SMessage });
        }

        [Authorize]
        [HttpGet("me")] 
        public IActionResult IsAuthorized()
        {
            var id = HttpContext.User.FindFirst("Id")?.Value;
            if (!string.IsNullOrWhiteSpace(id))
            {
                var result = _SearchUsersService.SearchUserByIdAsync(Convert.ToInt32(id)).Result;
                if(result != null)
                    return Ok();
            
            }
            return NotFound("Не Авторизован!");
        }
        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var result = _LoginService.LogOut().Result;
            if (result.SResultCode == EResultCode.Success)
                return Ok(new { message = result.SMessage });
            else
                return NotFound(new { message = result.SMessage });
        }
    }
} 