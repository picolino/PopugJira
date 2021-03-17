using FluentMigrator.Runner;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PopugJira.EventBus.Events.UserCud;
using PopugJira.GoalTracker.Consumers;
using PopugJira.GoalTracker.DataAccessLayer;
using PopugJira.Microservice;
using StartupBase = PopugJira.Microservice.StartupBase;

namespace PopugJira.GoalTracker
{
    public class Startup : StartupBase
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }
        
        protected override void ConfigureSwagger(SwaggerOptions options)
        {
            options.OpenApiTitle = "PopugJira.GoalTracker";
            options.OpenApiVersion = "v1";
            options.SwaggerDocName = "v1";
        }

        protected override string MicroserviceName => "GoalTracker";

        protected override void RegisterServices(IServiceCollection services)
        {
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

        protected override void RegisterApp(IApplicationBuilder app, IWebHostEnvironment env)
        {
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