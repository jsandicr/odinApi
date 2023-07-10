using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdinApi.Models;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;

namespace OdinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceModel _serviceModel;

        public ServiceController(IServiceModel serviceModel)
        {
            _serviceModel = serviceModel;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Service>> GetServices()
        {
            try
            {
                var services = _serviceModel.GetServices();
                return Ok(services);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Service>>> GetServiceById(int id)
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var service = _serviceModel.GetServiceById(id);
                if (service.id == 0)
                    return NotFound();
                return Ok(service);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Service>> PostService(Service service)
        {
            try
            {
                var response = _serviceModel.PostService(service);
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
        public async Task<ActionResult<Service>> PutService(int id, Service service)
        {
            try
            {
                service.id = id;
                var response = _serviceModel.PutService(service);
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
        public async Task<ActionResult<List<Service>>> DeleteService(int id)
        {
            try
            {
                var response = _serviceModel.DeleteService(id);
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
        [HttpGet("status/{status}")]
        public async Task<ActionResult<List<Service>>> GetServiceEstatus(bool status)
        {
            try
            {
                var response =  _serviceModel.GetServiceStatus(status);
                if (response != null)
                {
                    return response;
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("ListSubSerice/{id}")]
        [Authorize]
        public async Task<ActionResult<List<Service>>> GetListSubServicioById(long id) {
            try
            {
                var response = _serviceModel.GetListSubServicioById(id);
                if (response.Count > 0)
                {
                    return response;
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("FinalServices")]
        [Authorize]
        public async Task<ActionResult<List<Service>>> GetFinalServices()
        {
            try
            {
                var response = _serviceModel.GetFinalServices();
                if (response.Count > 0)
                {
                    return response;
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
