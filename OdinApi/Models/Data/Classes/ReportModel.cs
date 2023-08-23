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

        public List<Ticket> GetTicketsXTime(DateTime date1, DateTime date2)
        {
            var tickets = _context.Ticket
                        .Where(t => t.creationDate > date1 && t.creationDate < date2)
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

        public List<Ticket> GetTicketsXSupervisor(DateTime date1, DateTime date2)
        {
            var tickets = _context.Ticket
                        .Include(t => t.supervisor)
                        .Where(t => t.creationDate > date1 && t.creationDate < date2)
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

        public List<Ticket> GetTicketsXSupervisorM()
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

        public int GetCantTicketsAssigned(int id)
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

        public int GetCantTicketsOpen()
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
    }
}