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
        public async Task<IActionResult> CreatePrivateChat([FromBody] PrivateChatRequestDTO PrivateRequest)
        {
            var result = await _ChatService.CreatePrivateChat(PrivateRequest);
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
        public async Task<IActionResult> CreateGroupChat(GroupChatRequestDTO GroupRequest)
        {
            var result = await _ChatService.CreateGroupChat(GroupRequest);
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
        public async Task<IActionResult> GetChats()
        {
            var result = await _ChatService.GetChats();
            if (result.Item1.SResultCode == EResultCode.Success)
                return Ok(result.Item2);
            else
                return NotFound(new { message = result.Item1.SMessage });
        }
        [Authorize]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetChatsById(int Id)
        {
            var result = await _ChatService.GetChatById(Id);
            if (result.Item1.SResultCode == EResultCode.Success)
                return Ok(result.Item2);
            else
                return NotFound(new { message = result.Item1.SMessage });
        }
    }
} 