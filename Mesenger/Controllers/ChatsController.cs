using Mesenger.Api.DTO.RequestClasses;
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
        [HttpPost("private")]
        public IActionResult CreatePrivateChat([FromBody] PrivateChatRequest PrivateRequest)
        {
            var result = _ChatService.CreatePrivateChat(PrivateRequest).Result;
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
        [HttpPost("group/{Id}")]
        public IActionResult CreateGroupChat(GroupChatRequest GroupRequest)
        {
            var result = _ChatService.CreateGroupChat(GroupRequest).Result;
            if (result.Item2 == null) 
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
            var result = _ChatService.GetChats().Result;
            if (result.Item1.SResultCode == EResultCode.Success)
                return Ok(result.Item2);
            else
                return NotFound(new { message = result.Item1.SMessage });
        }
        [Authorize]
        [HttpGet("{Id}")]
        public IActionResult GetChatsById(int Id)
        {
            var result = _ChatService.GetChatById(Id).Result;
            if (result.Item1.SResultCode == EResultCode.Success)
                return Ok(result.Item2);
            else
                return NotFound(new { message = result.Item1.SMessage });
        }
    }
} 