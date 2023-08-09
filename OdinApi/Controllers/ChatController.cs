using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OdinApi.Models;
using OdinApi.Models.Data.Classes;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OdinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatModel _chatModel;
        private readonly IConfiguration _config;
        private readonly ITransactionalLogModel _transactionalLogModel;
        private readonly IErrorLogModel _logErrorModel;


        public ChatController(IChatModel chatModel, IConfiguration config, ITransactionalLogModel transactionalLogModel, IErrorLogModel logErrorModel)
        {
            _chatModel = chatModel;
            _config = config;
            _transactionalLogModel = transactionalLogModel;
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
                    //TransactionalLog log = new TransactionalLog();
                    //log.idUser = response.id;
                    //log.description = "Se creo el usuario" + response.mail;
                    //log.type = "Creación";
                    //log.date = DateTime.Now;
                    //log.module = "Usuario";
                    //_transactionalLogModel.PostTransactionalLog(log);
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
                    //TransactionalLog log = new TransactionalLog();
                    //log.idUser = int.Parse(User.FindFirstValue("id"));
                    //log.description = "Se actualizo el usuario " + response.mail;
                    //log.type = "Actulizacion de usuario ";
                    //log.date = DateTime.Now;
                    //log.module = "Usuario";
                    //_transactionalLogModel.PostTransactionalLog(log);
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
                    //TransactionalLog log = new TransactionalLog();
                    //log.idUser = int.Parse(User.FindFirstValue("id"));
                    //log.description = "Se cambio el estado usuario " + response.mail;
                    //log.type = "Cambio de estado";
                    //log.date = DateTime.Now;
                    //log.module = "Usuario";
                    //_transactionalLogModel.PostTransactionalLog(log);
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
