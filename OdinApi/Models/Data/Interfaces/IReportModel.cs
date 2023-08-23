using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Interfaces
{
    public interface IReportModel
    {
        public List<Ticket> GetTicketsXTime(DateTime date1, DateTime date2);
        public List<Ticket> GetTicketsXSupervisor(DateTime date1, DateTime date2);
        public List<Ticket> GetTicketsXSupervisorM();
        public int GetCantTicketsAssigned(int id);
        public int GetCantTicketsOpen();
    }
}