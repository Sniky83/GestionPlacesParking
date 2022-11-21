using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPlacesParking.Core.Infrastructure.Migrations
{
    public partial class AdminInserts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Insertions des Emails des Admins de l'applications
            migrationBuilder.InsertData(table: "Admin", column: "Email", value: "cohen@apside.fr");
            migrationBuilder.InsertData(table: "Admin", column: "Email", value: "borgo@apside.fr");
            migrationBuilder.InsertData(table: "Admin", column: "Email", value: "fdos-santos@apside.fr");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
