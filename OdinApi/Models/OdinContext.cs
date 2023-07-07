using Microsoft.EntityFrameworkCore;
using OdinApi.Models.Obj;

namespace OdinApi.Models
{
    public class OdinContext : DbContext
    {
        public OdinContext(DbContextOptions<OdinContext> opciones)
            : base(opciones)
        {
        }

        //Entidades
        public DbSet<User> User { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }
        public DbSet<TransactionalLog> TransactionalLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(User =>
            {
                User.ToTable("User");
                User.HasKey(x => x.id);
                User.Property(x => x.id)
                .ValueGeneratedOnAdd();
                User.Property(x => x.name)
                .IsRequired()
                .HasMaxLength(50);
                User.Property(x => x.lastName)
                .IsRequired()
                .HasMaxLength(50);
                User.Property(x => x.mail)
                .IsRequired()
                .HasMaxLength(50);
                User.Property(x => x.phone)
                .IsRequired();
                User.Property(x => x.active)
                .IsRequired();
            });

            modelBuilder.Entity<Ticket>(Ticket =>
            {
                Ticket.ToTable("Ticket");
                Ticket.HasKey(x => x.id);
                Ticket.Property(x => x.id)
                .ValueGeneratedOnAdd();
                Ticket.Property(x => x.title)
                .IsRequired()
                .HasMaxLength(50);
                Ticket.Property(x => x.description)
                .IsRequired()
                .HasMaxLength(100);
                Ticket.Property(x => x.creationDate)
                .IsRequired();
                Ticket.Property(x => x.active)
                .IsRequired();
            });

            modelBuilder.Entity<Branch>(Branch =>
            {
                Branch.ToTable("Branch");
                Branch.HasKey(x => x.id);
                Branch.Property(x => x.id)
                .ValueGeneratedOnAdd();
                Branch.Property(x => x.name)
                .IsRequired()
                .HasMaxLength(50);
                Branch.Property(x => x.direction)
                .IsRequired()
                .HasMaxLength(250);
                Branch.Property(x => x.active)
                .IsRequired();
            });

            modelBuilder.Entity<Service>(Service =>
            {
                Service.ToTable("Service");
                Service.HasKey(x => x.id);
                Service.Property(x => x.id)
                .ValueGeneratedOnAdd();
                Service.Property(x => x.name)
                .IsRequired()
                .HasMaxLength(50);
                Service.Property(x => x.description)
                .IsRequired()
                .HasMaxLength(250);
                Service.Property(x => x.active)
                .IsRequired();
                Service.Property(x => x.photo)
                .IsRequired();
                Service.Property(x => x.transport)
                .IsRequired();
                Service.Property(x => x.toAdministrator)
                .IsRequired();
            });

            modelBuilder.Entity<Rol>(Rol =>
            {
                Rol.ToTable("Rol");
                Rol.HasKey(x => x.id);
                Rol.Property(x => x.id)
                .ValueGeneratedOnAdd();
                Rol.Property(x => x.name)
                .IsRequired()
                .HasMaxLength(50);
                Rol.Property(x => x.description)
                .IsRequired()
                .HasMaxLength(250);
                Rol.Property(x => x.active)
                .IsRequired();
            });

            modelBuilder.Entity<Status>(Status =>
            {
                Status.ToTable("Status");
                Status.HasKey(x => x.id);
                Status.Property(x => x.id)
                .ValueGeneratedOnAdd();
                Status.Property(x => x.description)
                .IsRequired()
                .HasMaxLength(250);
                Status.Property(x => x.active)
                .IsRequired();
            });

            modelBuilder.Entity<Comment>(Comment =>
            {
                Comment.ToTable("Comment");
                Comment.HasKey(x => x.id);
                Comment.Property(x => x.id)
                .ValueGeneratedOnAdd();
                Comment.Property(x => x.description)
                .IsRequired()
                .HasMaxLength(100);
                Comment.Property(x => x.date)
                .IsRequired();
                Comment.Property(x => x.active)
                .IsRequired();
            });

            modelBuilder.Entity<Document>(Document =>
            {
                Document.ToTable("Document");
                Document.HasKey(x => x.id);
                Document.Property(x => x.id)
                .ValueGeneratedOnAdd();
                Document.Property(x => x.name)
                .IsRequired()
                .HasMaxLength(100);
                Document.Property(x => x.idUser)
                .IsRequired();
                Document.Property(x => x.idTicket)
                .IsRequired();
            });

            modelBuilder.Entity<ErrorLog>(ErrorLog =>
            {
                ErrorLog.ToTable("ErrorLog");
                ErrorLog.HasKey(x => x.id);
                ErrorLog.Property(x => x.id)
                .ValueGeneratedOnAdd();
                ErrorLog.Property(x => x.code)
                .IsRequired()
                .HasMaxLength(50);
                ErrorLog.Property(x => x.description)
                .IsRequired()
                .HasMaxLength(1000);
                ErrorLog.Property(x => x.date)
                .IsRequired();
            });

            modelBuilder.Entity<TransactionalLog>(TransactionalLog =>
            {
                TransactionalLog.ToTable("TransactionalLog");
                TransactionalLog.HasKey(x => x.id);
                TransactionalLog.Property(x => x.id)
                .ValueGeneratedOnAdd();
                TransactionalLog.Property(x => x.type)
                .IsRequired()
                .HasMaxLength(50);
                TransactionalLog.Property(x => x.description)
                .IsRequired()
                .HasMaxLength(1000);
                TransactionalLog.Property(x => x.module)
               .IsRequired()
               .HasMaxLength(50);
                TransactionalLog.Property(x => x.date)
                .IsRequired();
            });

            modelBuilder.Entity<User>().HasOne(x => x.rol)
                .WithMany(a => a.users)
                .HasForeignKey(l => l.idRol)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasOne(x => x.branch)
                .WithMany(a => a.users)
                .HasForeignKey(l => l.idBranch)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>().HasOne(x => x.client)
                .WithMany(a => a.ticketsC)
                .HasForeignKey(l => l.idClient)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Ticket>().HasOne(x => x.supervisor)
                .WithMany(a => a.ticketsS)
                .HasForeignKey(l => l.idSupervisor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>().HasOne(x => x.service)
                .WithMany(a => a.tickets)
                .HasForeignKey(l => l.idService)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Service>().HasOne(x => x.serviceMain)
                .WithMany(a => a.services)
                .HasForeignKey(l => l.idServiceMain)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>().HasOne(x => x.status)
                .WithMany(a => a.tickets)
                .HasForeignKey(l => l.idStatus)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>().HasOne(x => x.user)
                .WithMany(a => a.comments)
                .HasForeignKey(l => l.idUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>().HasOne(x => x.ticket)
                .WithMany(a => a.comments)
                .HasForeignKey(l => l.idTicket)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>().HasOne(x => x.user)
                .WithMany(a => a.documents)
                .HasForeignKey(l => l.idUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>().HasOne(x => x.ticket)
                .WithMany(a => a.documents)
                .HasForeignKey(l => l.idTicket)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ErrorLog>().HasOne(x => x.user)
                .WithMany(a => a.errorsLog)
                .HasForeignKey(l => l.idUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransactionalLog>().HasOne(x => x.user)
                .WithMany(a => a.transactionsLog)
                .HasForeignKey(l => l.idUser)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
