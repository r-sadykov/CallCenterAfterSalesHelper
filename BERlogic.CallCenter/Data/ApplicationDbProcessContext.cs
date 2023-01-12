using BERlogic.CallCenter.Models;
using Microsoft.EntityFrameworkCore;

namespace BERlogic.CallCenter.Data
{
    public class ApplicationDbProcessContext:DbContext
    {
        public ApplicationDbProcessContext(DbContextOptions<ApplicationDbProcessContext> options) : base(options)
        {
        }
        public DbSet<MailFilter> MailFilters { get; set; }
        public DbSet<FullReport> FullReports { get; set; }
        public DbSet<PassangerModel> Passangers { get; set; }
        public DbSet<LineRoute> LineRoutes { get; set; }
        public DbSet<ServiceOperationsModel> ServiceOperations { get; set; }
        public DbSet<GwiReceipt> GwiReceipts { get; set; }
        public DbSet<FullReportJournal> FullReportJournals { get; set; }

        /// <summary>
        ///     Configures the schema needed for the application identity framework.
        /// </summary>
        /// <param name="builder">The builder being used to construct the model for this application context.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FullReport>(entity => { entity.HasIndex(e => e.BookingNumber).IsUnique(true); });
            builder.Entity<GwiReceipt>(entity => { entity.HasIndex(e => e.SepaCode).IsUnique(true); });
            //base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
