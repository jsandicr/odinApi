namespace OdinApi.Models.Obj
{
    public class Service
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
        public string photo { get; set; }
        public string? requirements { get; set; }
        public int? idServiceMain { get; set; }
        public bool transport { get; set; }
        public bool toAdministrator { get; set; }
        public List<Ticket>? tickets { get; set; }
        //Sub Servicios de un Servicio principal
        public List<Service>? services { get; set; }
        public Service? serviceMain { get; set; }
    }
}
