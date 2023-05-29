using Microsoft.AspNetCore.Mvc;
using OdinApi.Models;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;

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
        public async Task<ActionResult<List<TransactionalLog>>> GetTransactionalLogById(int id)
        {
            //Retorna el Ok  que es igual al 200 (Status)
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TransactionalLog>>> DeleteTransactionalLog(int id)
        {
            try
            {
                var response = _transactionalLogModel.DeleteTransactionalLog(id);
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
    }
}
