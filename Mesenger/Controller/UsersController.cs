using Mesenger.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mesenger.Api.Controller
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISearchService _SearchService;


        public UsersController(ISearchService SearchService)
        {
            _SearchService = SearchService; 
        }
        [HttpGet]
        public IActionResult GetUsersBySearching([FromQuery] string search)
        {

        }



    }
}
