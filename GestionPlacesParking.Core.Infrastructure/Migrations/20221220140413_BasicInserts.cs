using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPlacesParking.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BasicInserts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Insertions basiques pour la table des places de parking
            migrationBuilder.Sql("DELETE FROM ParkingSlot", true);
            migrationBuilder.InsertData(table: "ParkingSlot", column: "Label", value: "P1");
            migrationBuilder.InsertData(table: "ParkingSlot", column: "Label", value: "P2");
            migrationBuilder.InsertData(table: "ParkingSlot", column: "Label", value: "P3");
            migrationBuilder.InsertData(table: "ParkingSlot", column: "Label", value: "P4");

            //Insertions basiques pour les users
            migrationBuilder.Sql("DELETE FROM [User]", true);
            migrationBuilder.InsertData(table: "User", columns: new[] { "Email", "FirstName", "LastName", "Password", "IsAdmin" }, values: new object[] { "j.martin@apside-groupe.com", "Jérome", "Martin", "25a2a4f0beea2f4a7f3053d235660b66a311c65f9acc5ecbf901e8157c681dd5", false });
            migrationBuilder.InsertData(table: "User", columns: new[] { "Email", "FirstName", "LastName", "Password", "IsAdmin" }, values: new object[] { "a.bouchet@apside-groupe.com", "Alban", "Bouchet", "e46db4b5ea934713d1d609de5499f411dd7cf98e72f426a98ab985fbf4f6fd06", true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
