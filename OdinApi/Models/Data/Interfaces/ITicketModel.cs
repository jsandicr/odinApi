﻿using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Interfaces
{
    public interface ITicketModel
    {
        public List<Ticket> GetTickets();
        public Ticket GetTicketById(int id);
        public Ticket PostTicket(Ticket ticket);
        public Ticket PutTicket(Ticket ticket);
        public Ticket DeleteTicket(int id);
        public List<Ticket> GetTicketAssignedById(int id);
        public List<Ticket> GetOpenTickets();
    }
}
