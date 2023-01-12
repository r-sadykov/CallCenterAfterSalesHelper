using Microsoft.EntityFrameworkCore.Migrations;

namespace BERlogic.CallCenter.Migrations.Processes
{
    public partial class EditPassangerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PassangerFee",
                table: "Passangers",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassangerFee",
                table: "Passangers");
        }
    }
}
