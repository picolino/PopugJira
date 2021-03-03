using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PopugJira.DataAccessLayer;

namespace PopugJira
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            var sqliteConnectionString = Configuration.GetConnectionString("SQLite");
            
            services.AddLinqToDbContext<SQLiteDatabaseConnection>((provider, options) =>
                                                            {
                                                                options.UseSQLite(sqliteConnectionString)
                                                                       .UseDefaultLogging(provider);
                                                            });

            services.AddFluentMigratorCore()
                    .ConfigureRunner(rb => rb.AddSQLite()
                                             .WithGlobalConnectionString(sqliteConnectionString)
                                             .ScanIn(typeof(DataAccessLayer.Migrations.MigrationsScanTarget).Assembly).For.Migrations())
                    .AddLogging(lb => lb.AddFluentMigratorConsole());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapBlazorHub();
                                 endpoints.MapFallbackToPage("/_Host");
                             });
            
            ProcessMigrations(app);
        }

        private static void ProcessMigrations(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var migrationsRunner = scope.ServiceProvider.GetService<IMigrationRunner>();
            if (migrationsRunner!.HasMigrationsToApplyUp())
            {
                migrationsRunner.MigrateUp();
            }
        }
    }
}