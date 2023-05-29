using Microsoft.AspNetCore.Mvc;
using OdinApi.Models;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Data.Classes;
using OdinApi.Models.Obj;

namespace OdinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchModel _branchModel;

        public BranchController(IBranchModel branchModel)
        {
            _branchModel = branchModel;
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
        public async Task<ActionResult<Branch>> PostBranch(Branch branch)
        {
            try
            {
                var response = _branchModel.PostBranch(branch);
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
        public async Task<ActionResult<Branch>> PutBranch(int id, Branch branch)
        {
            try
            {
                branch.id = id;
                var response = _branchModel.PutBranch(branch);
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
        public async Task<ActionResult<List<Branch>>> DeleteBranch(int id)
        {
            try
            {
                var response = _branchModel.DeleteBranch(id);
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
