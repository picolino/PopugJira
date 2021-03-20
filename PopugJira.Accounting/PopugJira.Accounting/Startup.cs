using System;
using FluentMigrator.Runner;
using FluentScheduler;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PopugJira.Accounting.DataAccessLayer;
using PopugJira.Accounting.Jobs;
using PopugJira.Microservice;
using StartupBase = PopugJira.Microservice.StartupBase;

namespace PopugJira.Accounting
{
    public class Startup : StartupBase
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureSwagger(SwaggerOptions options)
        {
            options.OpenApiTitle = "PopugJira.Accounting";
            options.OpenApiVersion = "v1";
            options.SwaggerDocName = "v1";
        }

        protected override string MicroserviceName => "Accounting";
        
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
            RegisterJobs(app.ApplicationServices);
        }

        private void RegisterJobs(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            var payEarnedJob = scope.ServiceProvider.GetService<PayEarnedToEmployeesJob>();
            
            JobManager.Initialize();
            JobManager.AddJob(payEarnedJob, o => o.ToRunNow().AndEvery(1).Days().At(20, 00));
        }
    }
}