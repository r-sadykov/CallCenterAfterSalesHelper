using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BERlogic.CallCenter.Migrations.Processes
{
    public partial class GwiReceiptClearanceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GwiReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookingCode = table.Column<string>(nullable: true),
                    Passanger = table.Column<string>(nullable: true),
                    BookingDate = table.Column<DateTime>(nullable: false),
                    ReceivedNoticeDate = table.Column<DateTime>(nullable: false),
                    ChangesDate = table.Column<DateTime>(nullable: false),
                    SepaCode = table.Column<string>(nullable: true),
                    SepaDate = table.Column<DateTime>(nullable: false),
                    SepaAmount = table.Column<decimal>(nullable: false),
                    MailContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GwiReceipts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GwiReceipts_SepaCode",
                table: "GwiReceipts",
                column: "SepaCode",
                unique: true,
                filter: "[SepaCode] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GwiReceipts");
        }
    }
}
