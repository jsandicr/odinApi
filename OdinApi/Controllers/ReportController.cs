using Microsoft.AspNetCore.Mvc;
using OdinApi.Models;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Data.Classes;
using OdinApi.Models.Obj;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace OdinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportModel _reportModel;
        private readonly IErrorLogModel _logErrorModel;

        public ReportController(IReportModel reportModel, IErrorLogModel errorLogModel)
        {
            _reportModel = reportModel;
            _logErrorModel = errorLogModel;

        }

        [HttpGet("TicketsXTime")]
        [Authorize]
        public async Task<ActionResult<List<Ticket>>> GetTicketsXTime()
        {
            try
            {
                var tickets = _reportModel.GetTicketsXTime();
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                ErrorLog error = new ErrorLog();
                error.description = ex.Message;
                error.date = DateTime.Now;
                error.code = ex.HResult;
                error.idUser = int.Parse(User.FindFirstValue("id")); ;
                _logErrorModel.PostErrorLog(error);
                return BadRequest();
            }
        }
        [Authorize]
        [HttpGet("TicketsXSupervisor")]
        public async Task<ActionResult<List<Ticket>>> GetTicketsXSupervisor()
        {
            try
            {
                var tickets = _reportModel.GetTicketsXSupervisor();
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                ErrorLog error = new ErrorLog();
                error.description = ex.Message;
                error.date = DateTime.Now;
                error.code = ex.HResult;
                error.idUser = int.Parse(User.FindFirstValue("id"));
                _logErrorModel.PostErrorLog(error);
                return BadRequest();
            }
        }
        [Authorize]
        [HttpGet("CantTicketsAssigned/{id}")]
        public async Task<ActionResult<int>> GetCantTicketsAssigned(int id)
        {
            try
            {
                var cantidad = _reportModel.GetCantTicketsAssigned(id);
                return Ok(cantidad);
            }
            catch (Exception ex)
            {
                ErrorLog error = new ErrorLog();
                error.description = ex.Message;
                error.date = DateTime.Now;
                error.code = ex.HResult;
                error.idUser = int.Parse(User.FindFirstValue("id"));
                _logErrorModel.PostErrorLog(error);
                return BadRequest();
            }
        }
        [Authorize]
        [HttpGet("CantTicketsOpen")]
        public async Task<ActionResult<int>> GetCantTicketsOpen()
        {
            try
            {
                var cantidad = _reportModel.GetCantTicketsOpen();
                return Ok(cantidad);
            }
            catch (Exception ex)
            {
                ErrorLog error = new ErrorLog();
                error.description = ex.Message;
                error.date = DateTime.Now;
                error.code = ex.HResult;
                error.idUser = int.Parse(User.FindFirstValue("id"));
                _logErrorModel.PostErrorLog(error);
                return BadRequest();
            }
        }
    }
}
