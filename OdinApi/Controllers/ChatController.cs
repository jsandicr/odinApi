using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;
using System.Security.Claims;

namespace OdinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatModel _chatModel;
        private readonly IErrorLogModel _logErrorModel;


        public ChatController(IChatModel chatModel, IErrorLogModel logErrorModel)
        {
            _chatModel = chatModel;
            _logErrorModel = logErrorModel;
        }

        [HttpGet]
        [Route("GetChat")]
        [Authorize]
        public async Task<ActionResult<List<Chat>>> GetChat()
        {
            try
            {
                var chats = _chatModel.GetChat();
                return Ok(chats);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<User>>> GetChatById(int id)
        {
            try
            {
                var chat = _chatModel.GetChatById(id);
                if (chat.Id == 0)
                    return NotFound();
                return Ok(chat);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<User>> PostChat(Chat chat)
        {
            try
            {
                var response = _chatModel.PostChat(chat);
                if (response.Id != 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                ErrorLog error = new ErrorLog();
                error.description = ex.Message;
                error.date = DateTime.Now;
                error.code = ex.HResult;
                error.idUser = int.Parse(User.FindFirstValue("id"));
                _logErrorModel.PostErrorLog(error);
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> PutChat(int id, Chat chat)
        {
            try
            {
                chat.Id = id;
                var response = _chatModel.PutChat(chat);
                if (response.Id != 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<List<User>>> DeleteChat(int id)
        {
            try
            {
                var response = _chatModel.DeleteChat(id);
                if (response != false)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
