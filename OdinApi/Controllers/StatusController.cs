using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdinApi.Models;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;

namespace OdinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusModel _statusModel;

        public StatusController(IStatusModel statusModel)
        {
            _statusModel = statusModel;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Status>> GetStatus()
        {
            try
            {
                var status = _statusModel.GetStatus();
                return Ok(status);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Status>>> GetStatusById(int id)
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var status = _statusModel.GetStatusById(id);
                if (status.id == 0)
                    return NotFound();
                return Ok(status);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Status>> PostStatus(Status status)
        {
            try
            {
                var response = _statusModel.PostStatus(status);
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
        public async Task<ActionResult<Status>> PutStatus(int id, Status status)
        {
            try
            {
                status.id = id;
                var response = _statusModel.PutStatus(status);
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
        [Authorize]
        public async Task<ActionResult<List<Status>>> DeleteStatus(int id)
        {
            try
            {
                var response = _statusModel.DeleteStatus(id);
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
