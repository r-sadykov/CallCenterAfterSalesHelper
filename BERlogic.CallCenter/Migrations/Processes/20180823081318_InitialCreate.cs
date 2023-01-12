using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BERlogic.CallCenter.Migrations.Processes
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.CreateTable(
                name: "FullReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookingNumber = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    SystemAgency = table.Column<string>(nullable: true),
                    DatevAgencyAccount = table.Column<string>(nullable: true),
                    Gds = table.Column<string>(nullable: true),
                    PassengerNames = table.Column<string>(nullable: true),
                    PassengerCount = table.Column<int>(nullable: false),
                    FirstAirline = table.Column<string>(nullable: true),
                    Ticket = table.Column<string>(nullable: true),
                    FirstGdsBookingNumber = table.Column<string>(nullable: true),
                    FirstGdsBookingAlias = table.Column<string>(nullable: true),
                    BookingDate = table.Column<DateTime>(nullable: false),
                    FirstRoute = table.Column<string>(nullable: true),
                    DepartureDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    Tariff = table.Column<decimal>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    FullScFlex = table.Column<decimal>(nullable: false),
                    BloPartScFlex = table.Column<decimal>(nullable: false),
                    PartnerPartScFlex = table.Column<decimal>(nullable: false),
                    BloFixSc = table.Column<decimal>(nullable: false),
                    PartnerFixSc = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    SellingCurrency = table.Column<string>(nullable: true),
                    ExchangeRateToEuro = table.Column<decimal>(nullable: false),
                    PaymentMethod = table.Column<string>(nullable: true),
                    SalesPoint = table.Column<string>(nullable: true),
                    Agent = table.Column<string>(nullable: true),
                    CardType = table.Column<string>(nullable: true),
                    CardHolder = table.Column<string>(nullable: true),
                    CustomerFirstName = table.Column<string>(nullable: true),
                    CustomerLastName = table.Column<string>(nullable: true),
                    CustomerGender = table.Column<string>(nullable: true),
                    CustomerCountry = table.Column<string>(nullable: true),
                    CustomerCity = table.Column<string>(nullable: true),
                    CustomerAddress = table.Column<string>(nullable: true),
                    CustomerEmail = table.Column<string>(nullable: true),
                    CustomerPhone = table.Column<string>(nullable: true),
                    NumberOfSegments = table.Column<int>(nullable: false),
                    ClearingType = table.Column<string>(nullable: true),
                    BookingClass = table.Column<string>(nullable: true),
                    FareBasis = table.Column<string>(nullable: true),
                    Commission = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullReports", x => x.Id);
                });
                */
                /*
            migrationBuilder.CreateTable(
                name: "MailFilters",
                columns: table => new
                {
                    MailFiltersId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MailAddress = table.Column<string>(nullable: false),
                    MailThemes = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailFilters", x => x.MailFiltersId);
                });
                */
            migrationBuilder.CreateTable(
                name: "ServiceOperations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookingDate = table.Column<DateTime>(nullable: false),
                    BookingNumber = table.Column<int>(nullable: false),
                    BookingCode = table.Column<string>(nullable: true),
                    ChangeServiceDate = table.Column<DateTime>(nullable: false),
                    CustomerFullName = table.Column<string>(nullable: true),
                    CustomerEmail = table.Column<string>(nullable: true),
                    PaymentMethod = table.Column<string>(nullable: true),
                    ClearingType = table.Column<string>(nullable: true),
                    AgencyCode = table.Column<string>(nullable: true),
                    Salespoint = table.Column<string>(nullable: true),
                    AgentName = table.Column<string>(nullable: true),
                    PaymentLink = table.Column<string>(nullable: true),
                    PriceDifference = table.Column<decimal>(nullable: false),
                    RebookingFee = table.Column<decimal>(nullable: false),
                    BerlogicFee = table.Column<decimal>(nullable: false),
                    ServiceOperations = table.Column<int>(nullable: false),
                    AirlineTotalPrice = table.Column<decimal>(nullable: false),
                    NonRefundableTaxes = table.Column<decimal>(nullable: false),
                    AdditionalServicesAndBaggage = table.Column<decimal>(nullable: false),
                    CancellationAirlineFee = table.Column<decimal>(nullable: false),
                    PrepaidBerlogicFee = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOperations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LineRoutes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DepartureDate = table.Column<DateTime>(nullable: false),
                    FlightAirline = table.Column<string>(nullable: true),
                    FlightNumber = table.Column<int>(nullable: false),
                    DepartureAirport = table.Column<string>(nullable: true),
                    ArrivalAirport = table.Column<string>(nullable: true),
                    DepartureTime = table.Column<DateTime>(nullable: false),
                    ArrivalDate = table.Column<DateTime>(nullable: false),
                    ArrivalTime = table.Column<DateTime>(nullable: false),
                    Baggage = table.Column<string>(nullable: true),
                    ServiceOperationsModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineRoutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineRoutes_ServiceOperations_ServiceOperationsModelId",
                        column: x => x.ServiceOperationsModelId,
                        principalTable: "ServiceOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Passangers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Prefix = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    ServiceOperationsModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passangers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passangers_ServiceOperations_ServiceOperationsModelId",
                        column: x => x.ServiceOperationsModelId,
                        principalTable: "ServiceOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineRoutes_ServiceOperationsModelId",
                table: "LineRoutes",
                column: "ServiceOperationsModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Passangers_ServiceOperationsModelId",
                table: "Passangers",
                column: "ServiceOperationsModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropTable(
                name: "FullReports");*/

            migrationBuilder.DropTable(
                name: "LineRoutes");
            /*
            migrationBuilder.DropTable(
                name: "MailFilters");*/

            migrationBuilder.DropTable(
                name: "Passangers");

            migrationBuilder.DropTable(
                name: "ServiceOperations");
        }
    }
}
