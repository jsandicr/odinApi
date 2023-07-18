using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace OdinApi.Models.Obj
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string mail { get; set; }
        public string phone { get; set; }
        public string photo { get; set; }
        public string password { get; set; }
        public bool active { get; set; }
        public bool restorePass { get; set; }
        public int idRol { get; set; }
        public int idBranch { get; set; }
        //La anotacio NotMapped se utiliza para que la propiedad no se cree en la base durante la migracion
        [NotMapped]
        public string? token { get; set; }
        public Rol? rol { get; set; }
        public Branch? branch { get; set; }
        public List<Ticket>? ticketsS { get; set; }
        public List<Ticket>? ticketsC { get; set; }
        public List<Comment>? comments { get; set; }
        public List<Document>? documents { get; set; }
        public List<ErrorLog>? errorsLog { get; set; }
        public List<TransactionalLog>? transactionsLog { get; set; }
    }
    public class RestorePassword
    {
        public string mail { get; set; }
        public string phone { get; set; }
    }
    public class ChangePassword
    {
        public int id { get; set; }
        public string  oldPassword { get; set; }
        public string password { get; set; }

        public string confirmpassword { get; set; }
    }
}
