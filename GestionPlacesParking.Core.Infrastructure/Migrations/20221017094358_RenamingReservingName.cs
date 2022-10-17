using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPlacesParking.Core.Infrastructure.Migrations
{
    public partial class RenamingReservingName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReservationName",
                table: "Reservation",
                newName: "ReservingName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReservingName",
                table: "Reservation",
                newName: "ReservationName");
        }
    }
}
