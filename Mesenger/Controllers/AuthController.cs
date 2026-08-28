using Mesenger.Api.DTO.RequestClasses;
using Messanger.Api.Enums;
using Messanger.Api.Services.Interfaces; 
using Messanger.DataAccess.Models; 
using Messenger.Api.Repository.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return DebugUserRepository.Users2;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegValid([FromBody] RegisterRequestDTO RegRequest)
        {
            var result = await _RegisterService.RegValidation(RegRequest);

            if (result.SResultCode == EResultCodes.Success)
                return Created();
            else
                return BadRequest(new { message = result.SMessage });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LogValidation([FromBody] LoginRequestDTO loginRequest)
        {
            var result = await _LoginService.LogValidation(loginRequest);
            if (result.SResultCode == EResultCodes.Success)
                return Created();
            else
                return BadRequest(new { message = result.SMessage });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> IsAuthorized()
        {
            var idStr = HttpContext.User.FindFirst("Id")?.Value;
            int MainId = int.TryParse(idStr, out int parsedId) ? parsedId : -1;

            if (MainId == -1)
                return Unauthorized(new { message = "Не авторизован" });

            var result = await _SearchUsersService.SearchUserByIdAsync(Convert.ToInt32(MainId));

            return Ok(result);
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _LoginService.LogOut();
            if (result.SResultCode == EResultCodes.Success)
                return Ok(new { message = result.SMessage });
            else
                return NotFound(new { message = result.SMessage });
        }
    }
}