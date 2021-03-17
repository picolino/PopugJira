using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using FluentMigrator.Runner;
using IdentityModel;
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
using PopugJira.EventBus;
using PopugJira.GoalTracker.Application;
using PopugJira.GoalTracker.Application.Commands;
using PopugJira.GoalTracker.DataAccessLayer;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using Serviced;

namespace PopugJira.GoalTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "PopugJira.GoalTracker", Version = "v1"}); });

            services.AddAuthentication("IdentityBearer")
                    .AddIdentityServerAuthentication("IdentityBearer",
                                                     options =>
                                                     {
                                                         options.Authority = "https://localhost:5005";
                                                         options.RequireHttpsMetadata = false;
                                                         options.RoleClaimType = ClaimTypes.Role;
                                                     });

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

            var assemblies = new[]
                             {
                                 typeof(DataAccessLayer.AutoDiTarget).Assembly,
                                 typeof(Application.AutoDiTarget).Assembly
                             };
            services.AddServiced(assemblies);
            
            var rabbitMessageBus = new RabbitMqMessageBus(RabbitHutch.CreateBus(Configuration.GetConnectionString("RabbitMQ")));
            services.AddSingleton<IMessageBus>(rabbitMessageBus);
            services.AddSingleton<RabbitScopedMessageDispatcher>();
            services.AddSingleton(p => new AutoSubscriber(rabbitMessageBus.Bus, "goal-tracker")
                                       {
                                           AutoSubscriberMessageDispatcher = new RabbitScopedMessageDispatcher(p)
                                       });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PopugJira.GoalTracker v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
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