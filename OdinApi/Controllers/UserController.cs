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
    public class UserController : ControllerBase
    {
        private readonly IUserModel _userModel;
        private readonly IConfiguration _config;

        public UserController(IUserModel rolModel, IConfiguration config)
        {
            _userModel = rolModel;
            _config = config;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
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

        [HttpGet]
        [Route("Client")]
        [Authorize]
        public async Task<ActionResult<User>> GetClients()
        {
            try
            {
                var users = _userModel.GetClients();
                return Ok(users);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("Supervisor")]
        [Authorize]
        public async Task<ActionResult<List<User>>> GetSupervisors()
        {
            try
            {
                var users = _userModel.GetSupervisors();
                return Ok(users);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<User>>> GetUserById(int id)
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var user = _userModel.GetUserById(id);
                if (user.id == 0)
                    return NotFound();
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Actualmente no lleva autorize porque se usa en el proceso de registro
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                var exist = _userModel.GetUserByMail(user.mail);
                if (exist.id != 0)
                {
                    return BadRequest();
                }
                var response = _userModel.PostUser(user);
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
        public async Task<ActionResult<User>> PutUser(int id, User user)
        {
            try
            {
                user.id = id;
                var response = _userModel.PutUser(user);
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
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            try
            {
                var response = _userModel.DeleteUser(id);
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

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<User>> Login(UserDTO userDTO)
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var user = _userModel.Login(userDTO);

                if (user.id == 0)
                    return NotFound();

                var jwt = _config.GetSection("JWT").Get<Jwt>();

                var rolName = user.rol.name;
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("id", user.id.ToString()),
                    new Claim(ClaimTypes.Role, rolName)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //Este token expira en 5 dias
                var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddHours(12),
                    signingCredentials: singIn
                );

                user.token = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("restorePassword/")]
        public async Task<ActionResult<User>> RestorePasword([FromBody] RestorePassword user)
        {
            try
            {
                var response = _userModel.RestorePasword(user);
                if (response != null)
                {
                    return Ok(response); // Devolver la respuesta con los datos del usuario
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

        [Authorize]
        [HttpPut("changePasswordd/")]
        public async Task<ActionResult<User>> ChangePassword(ChangePassword user) {

            try
            {
                var response = _userModel.ChangePassword(user);
                if (response != null)
                {
                    return Ok(response); 
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

        [Authorize]
        [HttpPost("GetSupervisorSucursal/")]
        public async Task<ActionResult<User>> GetSupervisorSucursal(int  idsucursal)
        {
            try
            {
                var response = _userModel.GetSupervisorSucursal(idsucursal);
                if (response != null)
                {
                    return Ok(response); // Devolver la respuesta con los datos del usuario
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
