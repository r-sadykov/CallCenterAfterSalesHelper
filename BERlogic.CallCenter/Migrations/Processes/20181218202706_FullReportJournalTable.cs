using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BERlogic.CallCenter.Migrations.Processes
{
    public partial class FullReportJournalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FullReportJournals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RegistrationEventDate = table.Column<DateTime>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    Pnr_Before = table.Column<string>(nullable: true),
                    Pnr_After = table.Column<string>(nullable: true),
                    BookingNumber_Before = table.Column<int>(nullable: false),
                    BookingNumber_After = table.Column<int>(nullable: false),
                    FileUploadDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullReportJournals", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FullReportJournals");
        }
    }
}
