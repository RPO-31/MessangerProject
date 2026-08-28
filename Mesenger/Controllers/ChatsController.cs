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
            if (result.SResultCode == EResultCodes.Success)
            {
                return Ok(new { message = result.SMessage });
            }
            else if (result.SResultCode == EResultCodes.ThisRoomAlreadyExist)
                return Ok(new { message = result.SMessage });

            else
            {
                return BadRequest(new { message = result.SMessage });
            }
        }

        [Authorize]
        [HttpPost("group/{Id}")]
        public async Task<IActionResult> CreateGroupChat(GroupChatRequestDTO GroupRequest)
        {
            var result = await _ChatService.CreateGroupChat(GroupRequest);

            if (result.SResultCode == EResultCodes.Success)
            {
                return Ok(new { message = result.SMessage });
            }
            else
            {
                return NotFound(new { message = result.SMessage });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            var result = await _ChatService.GetChats();
            if (result.Item1.SResultCode == EResultCodes.Success)
                return Ok(result.Item2);
            else
                return NotFound(new { message = result.Item1.SMessage });
        }
        [Authorize]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetChatsById(int Id)
        {
            var result = await _ChatService.GetChatById(Id);
            if (result.Item1.SResultCode == EResultCodes.Success)
                return Ok(result.Item2);
            else
                return BadRequest(new { message = result.Item1.SMessage });
        }
        [Authorize]
        [HttpGet("{Id}/messages")]
        public async Task<IActionResult> GetMessagesById(int Id)
        {
            var result = await _ChatService.GetChatMessages(Id);
            if (result.Item1.SResultCode == EResultCodes.Success)
                return Ok(result.Item2);
            else
                return BadRequest(new { message = result.Item1.SMessage });
        }
        [Authorize]
        [HttpPost("{Id}/messages")]
        public async Task<IActionResult> SendMessagesById(int Id, [FromBody] SendMsgRequestDTO SendMsgRequest)
        {
            var result = await _ChatService.SendChatMessages(Id, SendMsgRequest);
            if (result.SResultCode == EResultCodes.Success)
                return Ok(result);
            else
                return BadRequest(new { message = result.SMessage });
        }
    }
}