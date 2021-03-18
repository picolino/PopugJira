using FluentMigrator.Runner;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        protected override void ConfigureMigrator(IMigrationRunnerBuilder runnerBuilder)
        {
            var sqliteConnectionString = Configuration.GetConnectionString("SQLite");
            runnerBuilder.AddSQLite()
                         .WithGlobalConnectionString(sqliteConnectionString);
        }

        protected override void RegisterServices(IServiceCollection services)
        {
            var sqliteConnectionString = Configuration.GetConnectionString("SQLite");

            services.AddLinqToDbContext<SQLiteDatabaseConnection>((provider, options) =>
                                                                         {
                                                                             options.UseSQLite(sqliteConnectionString)
                                                                                    .UseDefaultLogging(provider);
                                                                         });
        }

        protected override void RegisterApp(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}