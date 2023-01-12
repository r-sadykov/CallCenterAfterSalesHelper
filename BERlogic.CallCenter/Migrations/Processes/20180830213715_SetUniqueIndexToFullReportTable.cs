using Microsoft.EntityFrameworkCore.Migrations;

namespace BERlogic.CallCenter.Migrations.Processes
{
    public partial class SetUniqueIndexToFullReportTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FullReports_BookingNumber",
                table: "FullReports",
                column: "BookingNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FullReports_BookingNumber",
                table: "FullReports");
        }
    }
}
