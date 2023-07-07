using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OdinApi.Migrations
{
    public partial class rdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<bool>(
                name: "toAdministrator",
                table: "Service",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "toAdministrator",
                table: "Service");
        }
    }
}
