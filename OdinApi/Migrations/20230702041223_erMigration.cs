using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OdinApi.Migrations
{
    public partial class erMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "transport",
                table: "Service",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
            name: "ubication",
            table: "Ticket",
            nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "transport",
                table: "Service");

            migrationBuilder.DropColumn(
            name: "ubication",
            table: "Ticket");
        }
    }
}
