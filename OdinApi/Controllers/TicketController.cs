﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdinApi.Models;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;

namespace OdinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketModel _ticketModel;

        public TicketController(ITicketModel ticketModel)
        {
            _ticketModel = ticketModel;
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

        [HttpGet("{id}")]
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
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            try
            {
                var response = _ticketModel.PostTicket(ticket);
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
        public async Task<ActionResult<Ticket>> PutTicket(int id, Ticket ticket)
        {
            try
            {
                ticket.id = id;
                var response = _ticketModel.PutTicket(ticket);
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
        public async Task<ActionResult<List<Ticket>>> DeleteTicket(int id)
        {
            try
            {
                var response = _ticketModel.DeleteTicket(id);
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
