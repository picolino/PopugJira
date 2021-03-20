using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PopugJira.Analytics.DataAccessLayer;
using PopugJira.Microservice;
using StartupBase = PopugJira.Microservice.StartupBase;

namespace PopugJira.Analytics
{
    public class Startup : StartupBase
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }
        
        protected override void ConfigureSwagger(SwaggerOptions options)
        {
            options.OpenApiTitle = "PopugJira.Analytics";
            options.OpenApiVersion = "v1";
            options.SwaggerDocName = "v1";
        }

        protected override string MicroserviceName => "Analytics";
        
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