using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPlacesParking.Core.Infrastructure.Migrations
{
    public partial class IsReservedIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsReserved",
                table: "Reservation",
                newName: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Reservation",
                newName: "IsReserved");
        }
    }
}
