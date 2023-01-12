﻿// <auto-generated />

using System;
using BERlogic.CallCenter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BERlogic.CallCenter.Migrations.Processes
{
    [DbContext(typeof(ApplicationDbProcessContext))]
    [Migration("20180830213715_SetUniqueIndexToFullReportTable")]
    partial class SetUniqueIndexToFullReportTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BERlogic.CallCenter.Models.FullReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Agent");

                    b.Property<decimal>("BloFixSc");

                    b.Property<decimal>("BloPartScFlex");

                    b.Property<string>("BookingClass");

                    b.Property<DateTime>("BookingDate");

                    b.Property<int>("BookingNumber");

                    b.Property<string>("CardHolder");

                    b.Property<string>("CardType");

                    b.Property<string>("ClearingType");

                    b.Property<string>("Commission");

                    b.Property<string>("CustomerAddress");

                    b.Property<string>("CustomerCity");

                    b.Property<string>("CustomerCountry");

                    b.Property<string>("CustomerEmail");

                    b.Property<string>("CustomerFirstName");

                    b.Property<string>("CustomerGender");

                    b.Property<string>("CustomerLastName");

                    b.Property<string>("CustomerPhone");

                    b.Property<string>("DatevAgencyAccount");

                    b.Property<DateTime>("DepartureDate");

                    b.Property<decimal>("ExchangeRateToEuro");

                    b.Property<string>("FareBasis");

                    b.Property<string>("FirstAirline");

                    b.Property<string>("FirstGdsBookingAlias");

                    b.Property<string>("FirstGdsBookingNumber");

                    b.Property<string>("FirstRoute");

                    b.Property<decimal>("FullScFlex");

                    b.Property<string>("Gds");

                    b.Property<int>("NumberOfSegments");

                    b.Property<decimal>("PartnerFixSc");

                    b.Property<decimal>("PartnerPartScFlex");

                    b.Property<int>("PassengerCount");

                    b.Property<string>("PassengerNames");

                    b.Property<string>("PaymentMethod");

                    b.Property<DateTime>("ReturnDate");

                    b.Property<string>("SalesPoint");

                    b.Property<string>("SellingCurrency");

                    b.Property<string>("Status");

                    b.Property<string>("SystemAgency");

                    b.Property<decimal>("Tariff");

                    b.Property<decimal>("Tax");

                    b.Property<string>("Ticket");

                    b.Property<decimal>("TotalPrice");

                    b.HasKey("Id");

                    b.HasIndex("BookingNumber")
                        .IsUnique();

                    b.ToTable("FullReports");
                });

            modelBuilder.Entity("BERlogic.CallCenter.Models.LineRoute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArrivalAirport");

                    b.Property<DateTime>("ArrivalDate");

                    b.Property<DateTime>("ArrivalTime");

                    b.Property<string>("Baggage");

                    b.Property<string>("DepartureAirport");

                    b.Property<DateTime>("DepartureDate");

                    b.Property<DateTime>("DepartureTime");

                    b.Property<string>("FlightAirline");

                    b.Property<int>("FlightNumber");

                    b.Property<int?>("ServiceOperationsModelId");

                    b.HasKey("Id");

                    b.HasIndex("ServiceOperationsModelId");

                    b.ToTable("LineRoutes");
                });

            modelBuilder.Entity("BERlogic.CallCenter.Models.MailFilter", b =>
                {
                    b.Property<int>("MailFiltersId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MailAddress")
                        .IsRequired();

                    b.Property<string>("MailThemes")
                        .IsRequired();

                    b.HasKey("MailFiltersId");

                    b.ToTable("MailFilters");
                });

            modelBuilder.Entity("BERlogic.CallCenter.Models.PassangerModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.Property<string>("Prefix");

                    b.Property<int?>("ServiceOperationsModelId");

                    b.HasKey("Id");

                    b.HasIndex("ServiceOperationsModelId");

                    b.ToTable("Passangers");
                });

            modelBuilder.Entity("BERlogic.CallCenter.Models.ServiceOperationsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AdditionalServicesAndBaggage");

                    b.Property<string>("AgencyCode");

                    b.Property<string>("AgentName");

                    b.Property<decimal>("AirlineTotalPrice");

                    b.Property<decimal>("BerlogicFee");

                    b.Property<string>("BookingCode");

                    b.Property<DateTime>("BookingDate");

                    b.Property<int>("BookingNumber");

                    b.Property<decimal>("CancellationAirlineFee");

                    b.Property<DateTime>("ChangeServiceDate");

                    b.Property<string>("ClearingType");

                    b.Property<string>("CustomerEmail");

                    b.Property<string>("CustomerFullName");

                    b.Property<decimal>("NonRefundableTaxes");

                    b.Property<string>("PaymentLink");

                    b.Property<string>("PaymentMethod");

                    b.Property<decimal>("PrepaidBerlogicFee");

                    b.Property<decimal>("PriceDifference");

                    b.Property<decimal>("RebookingFee");

                    b.Property<string>("Salespoint");

                    b.Property<int>("ServiceOperations");

                    b.HasKey("Id");

                    b.ToTable("ServiceOperations");
                });

            modelBuilder.Entity("BERlogic.CallCenter.Models.LineRoute", b =>
                {
                    b.HasOne("BERlogic.CallCenter.Models.ServiceOperationsModel")
                        .WithMany("LineRoutes")
                        .HasForeignKey("ServiceOperationsModelId");
                });

            modelBuilder.Entity("BERlogic.CallCenter.Models.PassangerModel", b =>
                {
                    b.HasOne("BERlogic.CallCenter.Models.ServiceOperationsModel")
                        .WithMany("PassangerModels")
                        .HasForeignKey("ServiceOperationsModelId");
                });
#pragma warning restore 612, 618
        }
    }
}