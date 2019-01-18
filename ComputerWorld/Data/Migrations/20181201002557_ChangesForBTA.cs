using Microsoft.EntityFrameworkCore.Migrations;

namespace ComputerWorld.Data.Migrations
{
    public partial class ChangesForBTA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isConfirmed",
                table: "Orders",
                newName: "BillToAccount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BillToAccount",
                table: "Orders",
                newName: "isConfirmed");
        }
    }
}
