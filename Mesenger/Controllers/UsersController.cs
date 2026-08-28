using Messanger.Api.Enums;
using Messanger.Api.Services.Interfaces;
using Messanger.Api.ViewModels; 
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;

namespace Messanger.Api.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISearchUsersService _SearchService;

        public UsersController(ISearchUsersService SearchService)
        {
            _SearchService = SearchService;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<UserViewModel>> SearchUsers([FromQuery] string name)
        {
            return await _SearchService.SearchUsersAsync(Uri.UnescapeDataString(name));
        }

    }
}