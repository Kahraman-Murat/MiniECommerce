using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniECommerce.Persistence.Migrations
{
    public partial class migr_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NamwSurname",
                table: "AspNetUsers",
                newName: "NameSurname");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameSurname",
                table: "AspNetUsers",
                newName: "NamwSurname");
        }
    }
}
