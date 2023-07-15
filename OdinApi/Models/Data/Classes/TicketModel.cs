﻿using Microsoft.EntityFrameworkCore;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;
using System.Net.Sockets;

namespace OdinApi.Models.Data.Classes
{
    public class TicketModel : ITicketModel
    {

        private readonly OdinContext _context;

        public TicketModel(OdinContext context)
        {
            _context = context;
        }
        public Ticket GetTicketById(int id)
        {
            try
            {
                var tickets = _context.Ticket
                            .Include(t => t.supervisor)
                            .Include(t => t.status)
                            .Include(t => t.client)
                            .Include(t => t.documents)
                            .Include(t => t.service)
                            .Include(t => t.comments)
                            .FirstOrDefault(t => t.id == id);

                
                if (tickets != null)
                {
                    return tickets;
                }
                else
                {
                    return new Ticket();
                }
            }
            catch (Exception)
            {
                return new Ticket();
            }
        }

        public List<Ticket> GetTicketAssignedById(int id)
        {
            try
            {
                //En join de comments se hizo de esa manera para hacer un left join en caso de que no hacen comentarios 
                var query = (from t in _context.Ticket
                             join c in _context.User
                             on t.idClient equals c.id
                             join s in _context.User
                             on t.idSupervisor equals s.id
                             join se in _context.Service
                             on t.idService equals se.id
                             join st in _context.Status
                             on t.idStatus equals st.id
                             join co in _context.Comment
                             on t.id equals co.idTicket into co_join
                             from p in co_join.DefaultIfEmpty()
                             join doc in _context.Document
                             on t.id equals doc.idTicket into doc_join
                             from d in doc_join.DefaultIfEmpty()
                             where s.id == id && t.active == true
                             select new { Ticket = t, Client = c, Supervisor = s, Service = se, Status = st, Comments = p, Documents = d }).ToList();

                if (query != null)
                {
                    List<Ticket> tickets = new List<Ticket>();
                    foreach (var q in query)
                    {
                        tickets.Add(q.Ticket);
                    }
                    return tickets;
                }
                else
                {
                    return new List<Ticket>();
                }
            }
            catch (Exception)
            {
                return new List<Ticket>();
            }
        }

        public List<Ticket> GetOpenTickets()
        {
            try
            {
                //En join de comments se hizo de esa manera para hacer un left join en caso de que no hacen comentarios 
                var query = (from t in _context.Ticket
                             join c in _context.User
                             on t.idClient equals c.id
                             join s in _context.User
                             on t.idSupervisor equals s.id
                             join se in _context.Service
                             on t.idService equals se.id
                             join st in _context.Status
                             on t.idStatus equals st.id
                             join co in _context.Comment
                             on t.id equals co.idTicket into co_join
                             from p in co_join.DefaultIfEmpty()
                             where t.closeDate == null
                             select new { Ticket = t, Client = c, Supervisor = s, Service = se, Status = st, Comments = p }).ToList();

                if (query != null)
                {
                    List<Ticket> tickets = new List<Ticket>();
                    foreach (var q in query)
                    {
                        tickets.Add(q.Ticket);
                    }
                    return tickets;
                }
                else
                {
                    return new List<Ticket>();
                }
            }
            catch (Exception)
            {
                return new List<Ticket>();
            }
        }

        public List<Ticket> GetTickets()
        {
            try
            {
                var query = (from t in _context.Ticket
                             join c in _context.User
                             on t.idClient equals c.id
                             join s in _context.User
                             on t.idSupervisor equals s.id
                             join se in _context.Service
                             on t.idService equals se.id
                             join st in _context.Status
                             on t.idStatus equals st.id
                             where t.active == true
                             select new { Ticket = t, Client = c, Supervisor = s, Service = se, Status = st }).ToList();

                if (query != null)
                {
                    List<Ticket> tickets = new List<Ticket>();
                    foreach (var q in query)
                    {
                        tickets.Add(q.Ticket);
                    }
                    return tickets;
                }
                else
                {
                    return new List<Ticket>();
                }
            }
            catch (Exception)
            {
                return new List<Ticket>();
            }
        }

        public Ticket PostTicket(Ticket ticket)
        {
            try
            {
                _context.Ticket.Add(ticket);
                _context.SaveChanges();
                return ticket;
            }
            catch (Exception)
            {
                return new Ticket();
            }
        }

        public Ticket DeleteTicket(int id)
        {
            try
            {
                Ticket ticket = _context.Ticket.Find(id);
                if (ticket != null)
                {
                    ticket.active = false;
                    _context.Update(ticket);
                    _context.SaveChanges();
                    return ticket;
                }
                else
                {
                    return new Ticket();
                }

            }
            catch (Exception)
            {
                return new Ticket();
            }
        }

        public Ticket PutTicket(Ticket ticket)
        {
            try
            {
                ticket.documents = null;
                ticket.comments = null;
                ticket.status = null;
                _context.Update(ticket);
                _context.SaveChanges();
                return ticket;
            }
            catch (Exception)
            {
                return new Ticket();
            }
        }

        public List<Ticket> GetTicketsClientsStatus(int id, string status)
        {
            try
            {
                var tickets = _context.Ticket
                .Include(t => t.supervisor)
                .Include(t => t.status)
                .Where(t => t.idClient == id && t.status.description.Equals(status))
                .ToList();

                if (tickets != null)
                {
                    return tickets;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
