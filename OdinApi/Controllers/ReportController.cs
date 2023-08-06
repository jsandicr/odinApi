using Microsoft.AspNetCore.Mvc;
using OdinApi.Models;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Data.Classes;
using OdinApi.Models.Obj;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace OdinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportModel _reportModel;

        public ReportController(IReportModel reportModel)
        {
            _reportModel = reportModel;
        }

        [HttpGet("TicketsXTime")]
        public async Task<ActionResult<List<Ticket>>> GetTicketsXTime()
        {
            try
            {
                var tickets = _reportModel.GetTicketsXTime();
                return Ok(tickets);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("TicketsXSupervisor")]
        public async Task<ActionResult<List<Ticket>>> GetTicketsXSupervisor()
        {
            try
            {
                var tickets = _reportModel.GetTicketsXSupervisor();
                return Ok(tickets);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("CantTicketsAssigned/{id}")]
        public async Task<ActionResult<int>> GetCantTicketsAssigned(int id)
        {
            try
            {
                var cantidad = _reportModel.GetCantTicketsAssigned(id);
                return Ok(cantidad);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("CantTicketsOpen")]
        public async Task<ActionResult<int>> GetCantTicketsOpen()
        {
            try
            {
                var cantidad = _reportModel.GetCantTicketsOpen();
                return Ok(cantidad);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
