using Microsoft.AspNetCore.Mvc;
using OdinApi.Models;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Data.Classes;
using OdinApi.Models.Obj;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace OdinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchModel _branchModel;
        private readonly ITransactionalLogModel _transactionalLogModel;

        public BranchController(IBranchModel branchModel, ITransactionalLogModel transactionalLogModel)
        {
            _branchModel = branchModel;
            _transactionalLogModel = transactionalLogModel;
        }

        [HttpGet]
        public async Task<ActionResult<Branch>> GetBranches()
        {
            try
            {
                var branches = _branchModel.GetBranches();
                return Ok(branches);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Branch>>> GetBranchById(int id)
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var branch = _branchModel.GetBranchById(id);
                if (branch.id == 0)
                    return NotFound();
                return Ok(branch);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Branch>> PostBranch(Branch branch)
        {
            try
            {
                int userId = int.Parse(User.FindFirstValue("id"));
                var response = _branchModel.PostBranch(branch);
                if (response.id != 0)
                {
                    TransactionalLog log = new TransactionalLog();
                    log.idUser = int.Parse(User.FindFirstValue("id"));
                    log.description = "Creación de nueva Sucursal";
                    log.type = "Crear";
                    log.date = DateTime.Now;
                    log.module = "Sucursales";
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

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Branch>> PutBranch(int id, Branch branch)
        {
            try
            {
                branch.id = id;
                var response = _branchModel.PutBranch(branch);
                if (response.id != 0)
                {
                    TransactionalLog log = new TransactionalLog();
                    log.idUser = int.Parse(User.FindFirstValue("id"));
                    log.description = "Actulización de Sucursal";
                    log.type = "Actulizar";
                    log.date = DateTime.Now;
                    log.module = "Sucursales";
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

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Branch>>> DeleteBranch(int id)
        {
            try
            {
                var response = _branchModel.DeleteBranch(id);
                if (response.id != 0)
                {
                    TransactionalLog log = new TransactionalLog();
                    log.idUser = int.Parse(User.FindFirstValue("id"));
                    log.description = "Cambio de estado de sucursal";
                    log.type = "Cambio Estado";
                    log.date = DateTime.Now;
                    log.module = "Sucursal";
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
