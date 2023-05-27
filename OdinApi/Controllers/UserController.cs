using Microsoft.AspNetCore.Mvc;
using OdinApi.Models;
using OdinApi.Models.Data;
using OdinApi.Models.Obj;

namespace OdinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserModel _userModel;

        public UserController(IUserModel rolModel)
        {
            _userModel = rolModel;
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUsuarios()
        {
            try
            {
                var users = _userModel.GetUsers();
                return Ok(users);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<User>>> GetUserById(int id)
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var user = _userModel.GetUserById(id);
                if (user == null)
                    return NotFound();
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                var response = _userModel.PostUser(user);
                if (response.id != null)
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

        [HttpPut]
        public async Task<ActionResult<User>> PutUser(User user)
        {
            try
            {
                var response = _userModel.PutUser(user);
                if (response.id != null)
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

        [HttpDelete]
        public async Task<ActionResult<List<User>>> DeleteUser(User user)
        {
            try
            {
                var response = _userModel.DeleteUser(user);
                if (response.id != null)
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
