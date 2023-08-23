using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;
using System.Security.Claims;

namespace OdinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionalLogController : ControllerBase
    {
        private readonly ITransactionalLogModel _transactionalLogModel;

        public TransactionalLogController(ITransactionalLogModel transactionalLogModel)
        {
            _transactionalLogModel = transactionalLogModel;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<TransactionalLog>> GetTransactionalLogs()
        {
            try
            {
                var transactionalLogs = _transactionalLogModel.GetTransactionalLogs();
                return Ok(transactionalLogs);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<TransactionalLog>>> GetTransactionalLogById(int id)
        {
            try
            {
                var transactionalLog = _transactionalLogModel.GetTransactionalLogById(id);
                if (transactionalLog.id == 0)
                    return NotFound();
                return Ok(transactionalLog);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<TransactionalLog>> PostTransactionalLog(TransactionalLog transactionalLog)
        {
            try
            {
                var response = _transactionalLogModel.PostTransactionalLog(transactionalLog);
                if (response.id != 0)
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

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<TransactionalLog>> PutTransactionalLog(int id, TransactionalLog transactionalLog)
        {
            try
            {
                transactionalLog.id = id;
                var response = _transactionalLogModel.PutTransactionalLog(transactionalLog);
                if (response.id != 0)
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

        [HttpDelete("{days}")]
        [Authorize]
        public async Task<ActionResult<List<TransactionalLog>>> DeleteTransactionalLog(int days)
        {
            try
            {
                var response = _transactionalLogModel.DeleteTransactionalLog(days);
                if (response)
                {
                    TransactionalLog log = new TransactionalLog();
                    log.idUser = int.Parse(User.FindFirstValue("id"));
                    log.description = "Se eliminaron registros con antigüedad mayor a " + days + " días";
                    log.type = "Eliminacion";
                    log.date = DateTime.Now;
                    log.module = "Transactional";
                    _transactionalLogModel.PostTransactionalLog(log);
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
