using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPlacesParking.Core.Infrastructure.Migrations
{
    public partial class BasicInserts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Insertions basiques pour la table des places de parking
            migrationBuilder.InsertData(table: "ParkingSlot", column: "Label", value: "P1");
            migrationBuilder.InsertData(table: "ParkingSlot", column: "Label", value: "P2");
            migrationBuilder.InsertData(table: "ParkingSlot", column: "Label", value: "P3");
            migrationBuilder.InsertData(table: "ParkingSlot", column: "Label", value: "P4");

            //Insertions basiques pour les users
            migrationBuilder.InsertData(table: "User", columns: new[] { "Email", "Password", "IsAdmin" }, values: new object[] { "user@apside-groupe.com", "25707515faae7c87cf20d7be987c15f5533867ee003f49011d5d2913d3f5ad6b", false }); ;
            migrationBuilder.InsertData(table: "User", columns: new[] { "Email", "Password", "IsAdmin" }, values: new object[] { "admin@apside-groupe.com", "a5d2db9447f30b6f256a774b034a6c7eb2041824bb8bff84b717db637699e5e5", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
