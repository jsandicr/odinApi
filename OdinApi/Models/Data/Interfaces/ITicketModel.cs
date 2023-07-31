using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Interfaces
{
    public interface ITicketModel
    {
        public List<Ticket> GetTickets();
        public Ticket GetTicketById(int id);
        public Ticket PostTicket(Ticket ticket);
        public Ticket PutTicket(Ticket ticket);
        public Ticket DeleteTicket(int id);
        public List<Ticket> GetTicketAssignedById(int id, string status);
        public List<Ticket> GetOpenTickets();
        public List<Ticket> GetTicketsClientsStatus(int id, string status);

        Task<List<Ticket>> GetTicketsByBranch(int branchId, string status);
    }
}
