#region Using

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using BERlogic.CallCenter.Configuration;
using BERlogic.CallCenter.Data;
using BERlogic.CallCenter.Models;
using BERlogic.CallCenter.Models.Repositories;
using BERlogic.CallCenter.Models.UserManagement.Interfaces;
using BERlogic.CallCenter.Models.UserManagement.Repository;
using BERlogic.CallCenter.Services;
using DinkToPdf;
using DinkToPdf.Contracts;
using JsonLocalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

// ReSharper disable UnusedMember.Global
// ReSharper disable once ClassNeverInstantiated.Global

#endregion

namespace BERlogic.CallCenter
{
    /// <summary>
    /// Defines the startup instance used by the web host.
    /// </summary>
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            // Expose the injected instance locally so we populate our settings instance
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Bind the settings instance as a singleton and expose it as an options type (IOptions<AppSettings>)
            // Note: This ensures that injecting both IOptions<T> and T is made possible and will resolve
            services.Configure<AppSettings>(Configuration);

            // Bind the settings instance as a singleton and expose it as an options type (IOptions<SmartSettings>)
            services.Configure<SmartSettings>(Configuration.GetSection("SmartAdmin"));

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("de-DE"),
                    new CultureInfo("ru-RU"),
                };
                options.DefaultRequestCulture = new RequestCulture("de-DE");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            // We retrieve the current bound AppSettings instance in order to access the connection string
            // Note: While this does performs a model binding to the type, it does not modify the service collection
            var settings = Configuration.Get<AppSettings>();

            // We allow our routes to be in lowercase
            services.AddRouting(options => options.LowercaseUrls = true);

            // We will setup this simple seeding helper to ensure default data is present
            services.AddTransient<ApplicationDbSeeder>();

            // Enable the use of SQL Server utilizing DI
            services.AddEntityFrameworkSqlServer();

            // Add the default identity classes and schema for use with EntityFramework
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserSettings>();
            // Enable the Context pool to manage the connections in an optimized manner
            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(settings.ConnectionString));

            var sp = services.BuildServiceProvider();
            var dbCore = sp.GetService<ApplicationDbContext>();
            try
            {
                if (dbCore.DatabaseConfigurations.LastOrDefault() != null)
                {
                    services.AddDbContextPool<ApplicationDbProcessContext>(options =>
                        options.UseSqlServer(dbCore.DatabaseConfigurations.LastOrDefault()?.ConnectionString ??
                                             throw new InvalidOperationException()));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IDatabaseConfiguration, DatabaseConfigurationRepository>();
            services.AddTransient<IMailServerConfiguration, MailServerConfigurationRepository>();
            services.AddTransient<IMailClientConfiguration, MailClientConfigurationRepository>();
            services.AddTransient<IMailFilter, MailFilterRepository>();
            services.AddTransient<IFullReport, FullReportRepository>();
            services.AddTransient<IServiceOperations, ServiceOperationsRepository>();
            services.AddSingleton<ITempedServiceOperations, TempedServiceOperationsRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddTransient<IGwiReceiptClearance, GwiReceiptsRepository>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload")));
            services.AddTransient<IFileInFolder, FileInFolderRepository>();
            services.AddTransient<IFileReportJournal, FileReportJournalInDbRepository>();
            services.AddTransient<IUsersInRole, UsersInRoleRepository>();

            // Cache 200 (OK) server responses; any other responses, including error pages, are ignored.
            services.AddResponseCaching();

            services.AddMemoryCache();
            services.AddSession();
            services.AddJsonLocalization();


            // We need essential Mvc services and DI support to host the template pages
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddControllersAsServices()
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, options => { options.ResourcesPath = "Resources"; })
                    .AddDataAnnotationsLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbSeeder dbSeeder)
        {
            // We detect if we are doing local development
            if (env.IsDevelopment())
            {
                // If this is the case then enable more detailed error output
                app.UseDeveloperExceptionPage();

                // Optionally enable browser link integration
                app.UseBrowserLink();

                // Display a more specific error page when an exception occurs connecting to the database
                app.UseDatabaseErrorPage();
            }
            else
            {
                // If this is not the case then forward the error to our generic view
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Warning: Do not trigger this seed in your production environment, this is a security risk!
            if (!env.IsProduction())
                // Ensure we have the default user added to the store
                dbSeeder.EnsureSeed().GetAwaiter().GetResult();


            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            // Ensures we can serve static-files that should not be processed by ASP.NET
            app.UseStaticFiles();

            // Enable the authentication middleware so we can protect access to controllers and/or actions
            app.UseAuthentication();

            // Enable the reponse caching middleware to serve 200 OK responses directly from cache on sub-sequent requests
            app.UseResponseCaching();

            app.UseSession();

            // We rely on the MVC pipeline to handle our routes
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                                name: "default",
                                template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
