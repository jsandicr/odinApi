using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdinApi.Models;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;

namespace OdinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolModel _rolModel;

        public RolController(IRolModel rolModel)
        {
            _rolModel = rolModel;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Rol>> GetRoles()
        {
            try
            {
                var roles = _rolModel.GetRoles();
                return Ok(roles);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Rol>>> GetRolById(int id)
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var rol = _rolModel.GetRolById(id);
                if (rol.id == 0)
                    return NotFound();
                return Ok(rol);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Este endpoint actualmente no es autorize debido a que se utiliza en el proceso de registro
        [HttpGet]
        [Route("First")]
        public async Task<ActionResult<List<Rol>>> GetFirstRol()
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var rol = _rolModel.GetFirstRol();
                if (rol.id == 0)
                    return NotFound();
                return Ok(rol);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Rol>> PostRol(Rol Rol)
        {
            try
            {
                var response = _rolModel.PostRol(Rol);
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
        public async Task<ActionResult<Rol>> PutRol(int id, Rol rol)
        {
            try
            {
                rol.id = id;
                var response = _rolModel.PutRol(rol);
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
        public async Task<ActionResult<List<Rol>>> DeleteRol(int id)
        {
            try
            {
                var response = _rolModel.DeleteRol(id);
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
