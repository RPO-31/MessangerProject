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
        public async Task<IActionResult> RegValid([FromBody] RegisterRequestDTO RegRequest)
        {
            var result = await _RegisterService.RegValidation(RegRequest);

            if (result.SResultCode == EResultCode.Success)
                return Created();
            else
                return BadRequest( new { message = result.SMessage } );
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LogValidation([FromBody] LoginRequestDTO loginRequest)
        {
            var result = await _LoginService.LogValidation(loginRequest);
            if (result.SResultCode == EResultCode.Success)
                return Created();
            else
                return BadRequest(new { message = result.SMessage });
        }

        [Authorize]
        [HttpGet("me")] 
        public async Task<IActionResult> IsAuthorized()
        {
            var id = HttpContext.User.FindFirst("Id")?.Value;
            if (!string.IsNullOrWhiteSpace(id))
            {
                var result = await _SearchUsersService.SearchUserByIdAsync(Convert.ToInt32(id));
                if(result != null)
                    return Ok();
            
            }
            return NotFound("Не Авторизован!");
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _LoginService.LogOut();
            if (result.SResultCode == EResultCode.Success)
                return Ok(new { message = result.SMessage });
            else
                return NotFound(new { message = result.SMessage });
        }
    }
} 