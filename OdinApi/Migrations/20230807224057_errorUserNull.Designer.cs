﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OdinApi.Models;

#nullable disable

namespace OdinApi.Migrations
{
    [DbContext(typeof(OdinContext))]
    [Migration("20230807224057_errorUserNull")]
    partial class errorUserNull
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OdinApi.Models.Obj.Branch", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<string>("direction")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("id");

                    b.ToTable("Branch", (string)null);
                });

            modelBuilder.Entity("OdinApi.Models.Obj.Comment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("idTicket")
                        .HasColumnType("int");

                    b.Property<int>("idUser")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("idTicket");

                    b.HasIndex("idUser");

                    b.ToTable("Comment", (string)null);
                });

            modelBuilder.Entity("OdinApi.Models.Obj.Document", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"), 1L, 1);

                    b.Property<int>("idTicket")
                        .HasColumnType("int");

                    b.Property<int>("idUser")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("nameDocument")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("idTicket");

                    b.HasIndex("idUser");

                    b.ToTable("Document", (string)null);
                });

            modelBuilder.Entity("OdinApi.Models.Obj.ErrorLog", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("code")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("idUser")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("idUser");

                    b.ToTable("ErrorLog", (string)null);
                });

            modelBuilder.Entity("OdinApi.Models.Obj.Rol", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id");

                    b.ToTable("Rol", (string)null);
                });

            modelBuilder.Entity("OdinApi.Models.Obj.Service", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("idServiceMain")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("requirements")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("toAdministrator")
                        .HasColumnType("bit");

                    b.Property<bool>("transport")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.HasIndex("idServiceMain");

                    b.ToTable("Service", (string)null);
                });

            modelBuilder.Entity("OdinApi.Models.Obj.Status", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("id");

                    b.ToTable("Status", (string)null);
                });

            modelBuilder.Entity("OdinApi.Models.Obj.Ticket", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("closeDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("creationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("estimatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("idClient")
                        .HasColumnType("int");

                    b.Property<int>("idService")
                        .HasColumnType("int");

                    b.Property<int>("idStatus")
                        .HasColumnType("int");

                    b.Property<int>("idSupervisor")
                        .HasColumnType("int");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ubication")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("idClient");

                    b.HasIndex("idService");

                    b.HasIndex("idStatus");

                    b.HasIndex("idSupervisor");

                    b.ToTable("Ticket", (string)null);
                });

            modelBuilder.Entity("OdinApi.Models.Obj.TransactionalLog", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("idUser")
                        .HasColumnType("int");

                    b.Property<string>("module")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id");

                    b.HasIndex("idUser");

                    b.ToTable("TransactionalLog", (string)null);
                });

            modelBuilder.Entity("OdinApi.Models.Obj.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<int>("idBranch")
                        .HasColumnType("int");

                    b.Property<int>("idRol")
                        .HasColumnType("int");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("mail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("restorePass")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.HasIndex("idBranch");

                    b.HasIndex("idRol");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("OdinApi.Models.Obj.Comment", b =>
                {
                    b.HasOne("OdinApi.Models.Obj.Ticket", "ticket")
                        .WithMany("comments")
                        .HasForeignKey("idTicket")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OdinApi.Models.Obj.User", "user")
                        .WithMany("comments")
                        .HasForeignKey("idUser")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ticket");

                    b.Navigation("user");
                });

            modelBuilder.Entity("OdinApi.Models.Obj.Document", b =>
                {
                    b.HasOne("OdinApi.Models.Obj.Ticket", "ticket")
                        .WithMany("documents")
                        .HasForeignKey("idTicket")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OdinApi.Models.Obj.User", "user")
                        .WithMany("documents")
                        .HasForeignKey("idUser")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ticket");

                    b.Navigation("user");
                });

            modelBuilder.Entity("OdinApi.Models.Obj.ErrorLog", b =>
                {
                    b.HasOne("OdinApi.Models.Obj.User", "user")
                        .WithMany("errorsLog")
                        .HasForeignKey("idUser")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("user");
                });

            modelBuilder.Entity("OdinApi.Models.Obj.Service", b =>
                {
                    b.HasOne("OdinApi.Models.Obj.Service", "serviceMain")
                        .WithMany("services")
                        .HasForeignKey("idServiceMain")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("serviceMain");
                });

            modelBuilder.Entity("OdinApi.Models.Obj.Ticket", b =>
                {
                    b.HasOne("OdinApi.Models.Obj.User", "client")
                        .WithMany("ticketsC")
                        .HasForeignKey("idClient")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OdinApi.Models.Obj.Service", "service")
                        .WithMany("tickets")
                        .HasForeignKey("idService")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OdinApi.Models.Obj.Status", "status")
                        .WithMany("tickets")
                        .HasForeignKey("idStatus")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OdinApi.Models.Obj.User", "supervisor")
                        .WithMany("ticketsS")
                        .HasForeignKey("idSupervisor")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("client");

                    b.Navigation("service");

                    b.Navigation("status");

                    b.Navigation("supervisor");
                });

            modelBuilder.Entity("OdinApi.Models.Obj.TransactionalLog", b =>
                {
                    b.HasOne("OdinApi.Models.Obj.User", "user")
                        .WithMany("transactionsLog")
                        .HasForeignKey("idUser")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("user");
                });

            modelBuilder.Entity("OdinApi.Models.Obj.User", b =>
                {
                    b.HasOne("OdinApi.Models.Obj.Branch", "branch")
                        .WithMany("users")
                        .HasForeignKey("idBranch")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OdinApi.Models.Obj.Rol", "rol")
                        .WithMany("users")
                        .HasForeignKey("idRol")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("branch");

                    b.Navigation("rol");
                });

            modelBuilder.Entity("OdinApi.Models.Obj.Branch", b =>
                {
                    b.Navigation("users");
                });

            modelBuilder.Entity("OdinApi.Models.Obj.Rol", b =>
                {
                    b.Navigation("users");
                });

            modelBuilder.Entity("OdinApi.Models.Obj.Service", b =>
                {
                    b.Navigation("services");

                    b.Navigation("tickets");
                });

            modelBuilder.Entity("OdinApi.Models.Obj.Status", b =>
                {
                    b.Navigation("tickets");
                });

            modelBuilder.Entity("OdinApi.Models.Obj.Ticket", b =>
                {
                    b.Navigation("comments");

                    b.Navigation("documents");
                });

            modelBuilder.Entity("OdinApi.Models.Obj.User", b =>
                {
                    b.Navigation("comments");

                    b.Navigation("documents");

                    b.Navigation("errorsLog");

                    b.Navigation("ticketsC");

                    b.Navigation("ticketsS");

                    b.Navigation("transactionsLog");
                });
#pragma warning restore 612, 618
        }
    }
}
