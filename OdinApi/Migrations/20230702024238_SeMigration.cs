using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OdinApi.Migrations
{
    public partial class SeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idServiceMain",
                table: "Service",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "requirements",
                table: "Service",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_idServiceMain",
                table: "Service",
                column: "idServiceMain");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Service_idServiceMain",
                table: "Service",
                column: "idServiceMain",
                principalTable: "Service",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Service_idServiceMain",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_idServiceMain",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "idServiceMain",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "requirements",
                table: "Service");
        }
    }
}
