using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(Email request);
        void SendUser(User user,string password);
        void SendEmailNew(Ticket ticket);
    }
}
