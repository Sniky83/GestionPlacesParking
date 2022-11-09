using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPlacesParking.Core.Infrastructure.Migrations
{
    public partial class ProprietaireIdReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProprietaireId",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProprietaireId",
                table: "Reservation");
        }
    }
}
