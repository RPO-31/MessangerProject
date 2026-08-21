using Messanger.Api.Services.Interfaces;
using Messanger.Api.ViewModels;
using Messanger.DataAccess.Models;
using Messenger.Repository.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<List<UserViewModel>> SearchUsers([FromQuery]string search)
        {
            return await _SearchService.SearchUsersAsync(search);
        }

    }
} 