using Mesenger.Api.Services.Interfaces;
using Messanger.Api.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mesenger.Api.Controllers
{
    [Route("chats")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService _ChatService;

        public ChatsController(IChatService ChatService)
        {
            _ChatService = ChatService;
        }

        [Authorize]
        [HttpPost("private/{Id}")]
        public IActionResult CreatePrivateChat(int Id)
        {
            var result = _ChatService.CreatePrivateChat(Id).Result;
            if(result.Item2 == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetChats()
        {
            var result = _ChatService.GetChats();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }
        [Authorize]
        [HttpGet("{Id}")]
        public IActionResult GetChatsById(int Id)
        {
            var result = _ChatService.GetChats();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }
    }
} 