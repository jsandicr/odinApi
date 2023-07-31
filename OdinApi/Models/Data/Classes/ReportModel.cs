using Microsoft.EntityFrameworkCore;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Classes
{
    public class ReportModel : IReportModel
    {

        private readonly OdinContext _context;

        public ReportModel(OdinContext context)
        {
            _context = context;
        }
        public List<Ticket> GetTicketsXTime()
        {
            try
            {
                var tickets = _context.Ticket
                            .OrderByDescending(t => t.creationDate)
                            .ToList();

                if (tickets != null)
                {
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

        public List<Ticket> GetTicketsXSupervisor()
        {
            try
            {
                var tickets = _context.Ticket
                            .Include(t => t.supervisor)
                            .OrderByDescending(t => t.idSupervisor)
                            .Where(t => t.closeDate == null)
                            .ToList();

                if (tickets != null)
                {
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

        public int GetCantTicketsAssigned(int id)
        {
            try
            {
                var tickets = _context.Ticket
                            .Include(t => t.client)
                            .Include(t => t.status)
                            .Include(t => t.service)
                            .Where(t => t.idSupervisor == id)
                            .ToList();

                if (tickets != null)
                {
                    return tickets.Count();
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int GetCantTicketsOpen()
        {
            try
            {
                var tickets = _context.Ticket
                            .Include(t => t.client)
                            .Include(t => t.status)
                            .Include(t => t.service)
                            .Where(t => t.closeDate == null)
                            .ToList();

                if (tickets != null)
                {
                    return tickets.Count();
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
