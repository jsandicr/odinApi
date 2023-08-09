using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdinApi.Models;
using OdinApi.Models.Data.Classes;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;
using System.Security.Claims;

namespace OdinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketModel _ticketModel;
        private readonly ITransactionalLogModel _transactionalLogModel;

        public TicketController(ITicketModel ticketModel, ITransactionalLogModel transactionalLogModel)
        {
            _ticketModel = ticketModel;
            _transactionalLogModel = transactionalLogModel;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Ticket>> GetTickets()
        {
            try
            {
                var tickets = _ticketModel.GetTickets();
                return Ok(tickets);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("Assigned/{id},{status}")]
        [Authorize]
        public async Task<ActionResult<List<Ticket>>> GetTicketAssignedById(int id,string status)
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var tickets = _ticketModel.GetTicketAssignedById(id, status);
                return Ok(tickets);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("Open")]
        [Authorize]
        public async Task<ActionResult<List<Ticket>>> GetOpenTickets()
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var tickets = _ticketModel.GetOpenTickets();
                return Ok(tickets);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetTicketsClients/{id},{status}")]
        public ActionResult<List<Ticket>> GetTicketsClientsStatus(int id, string status)
        {
            try
            {
                var tickets = _ticketModel.GetTicketsClientsStatus(id, status);
                return Ok(tickets);
            }
            catch (Exception ex)
            {

                return BadRequest("Ocurrió un error al obtener los tickets de los clientes.");
            }
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Ticket>>> GetTicketById(int id)
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var ticket = _ticketModel.GetTicketById(id);
                if (ticket.id == 0)
                    return NotFound();
                return Ok(ticket);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            try
            {
                var response = _ticketModel.PostTicket(ticket);
                if (response.id != 0)
                {
                    TransactionalLog log = new TransactionalLog();
                    log.idUser = int.Parse(User.FindFirstValue("id"));
                    log.description = "Creación de nuevo Tiquete con código Cod-"+ticket.id;
                    log.type = "Crear";
                    log.date = DateTime.Now;
                    log.module = "Tiquete";
                    _transactionalLogModel.PostTransactionalLog(log);
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

        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult<Ticket>> PutTicket(int id, Ticket ticket)
        {
            try
            {
                ticket.id = id;
                //ticket.client.documents = null;
           
                var response = _ticketModel.PutTicket(ticket);
                if (response.id != 0)
                {
                    TransactionalLog log = new TransactionalLog();
                    log.idUser = int.Parse(User.FindFirstValue("id"));
                    log.description = "Actulizacion de Tiquete con código Cod-" + ticket.id;
                    log.type = "Crear";
                    log.date = DateTime.Now;
                    log.module = "Tiquete";
                    _transactionalLogModel.PostTransactionalLog(log);
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

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Ticket>>> DeleteTicket(int id)
        {
            try
            {
                var response = _ticketModel.DeleteTicket(id);
                if (response.id != 0)
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

        [HttpGet]
        [Route("Sucursal/{idbranch},{status}")]
        [Authorize]
        public async Task<ActionResult<List<Ticket>>> GetTicketsByBranch(int idbranch, string status)
        {
            try
            {
                var response = await _ticketModel.GetTicketsByBranch(idbranch, status);
                if (response!=null)
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
    }
}
