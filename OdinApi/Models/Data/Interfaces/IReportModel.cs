using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Interfaces
{
    public interface IReportModel
    {
        public List<Ticket> GetTicketsXTime();
        public List<Ticket> GetTicketsXSupervisor();
        public int GetCantTicketsAssigned(int id);
        public int GetCantTicketsOpen();
    }
}
