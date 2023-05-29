using Microsoft.AspNetCore.Mvc;
using OdinApi.Models;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;

namespace OdinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorLogController : ControllerBase
    {
        private readonly IErrorLogModel _errorLogModel;

        public ErrorLogController(IErrorLogModel errorLogModel)
        {
            _errorLogModel = errorLogModel;
        }

        [HttpGet]
        public async Task<ActionResult<ErrorLog>> GetErrorLogs()
        {
            try
            {
                var errorLogs = _errorLogModel.GetErrorLogs();
                return Ok(errorLogs);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ErrorLog>>> GetErrorLogById(int id)
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var errorLog = _errorLogModel.GetErrorLogById(id);
                if (errorLog == null)
                    return NotFound();
                return Ok(errorLog);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ErrorLog>> PostErrorLog(ErrorLog errorLog)
        {
            try
            {
                var response = _errorLogModel.PostErrorLog(errorLog);
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
        public async Task<ActionResult<ErrorLog>> PutErrorLog(int id, ErrorLog errorLog)
        {
            try
            {
                errorLog.id = id;
                var response = _errorLogModel.PutErrorLog(errorLog);
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
        public async Task<ActionResult<List<ErrorLog>>> DeleteErrorLog(int id)
        {
            try
            {
                var response = _errorLogModel.DeleteErrorLog(id);
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
