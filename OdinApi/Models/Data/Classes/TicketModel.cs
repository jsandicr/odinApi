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
                             where t.id == id
                             select new { Ticket = t, Client = c, Supervisor = s, Service = se, Status = st, Comments = p }).ToList();

                if (query.Count > 0)
                {
                    return query.FirstOrDefault().Ticket;
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
                    _context.Remove(ticket);
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
                _context.Update(ticket);
                _context.SaveChanges();
                return ticket;
            }
            catch (Exception)
            {
                return new Ticket();
            }
        }
    }
}
