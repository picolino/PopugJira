using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PopugJira.EventBus;
using Serviced;

namespace PopugJira.Microservice
{
    public abstract class StartupBase
    {
        protected StartupBase(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected IConfiguration Configuration { get; }

        protected abstract void ConfigureSwagger(SwaggerOptions options);
        protected abstract string MicroserviceName { get; }
        protected abstract void ConfigureMigrator(IMigrationRunnerBuilder runnerBuilder);
        protected abstract void RegisterServices(IServiceCollection services);
        protected abstract void RegisterApp(IApplicationBuilder app, IWebHostEnvironment env);
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var domainAssemblies = LoadAllDomainAssemblies().ToArray();
            services.AddServiced(domainAssemblies);
            
            services.AddControllers();

            var swaggerOptions = new SwaggerOptions();
            ConfigureSwagger(swaggerOptions);
            services.AddSwaggerGen(c => { c.SwaggerDoc(swaggerOptions.SwaggerDocName, new OpenApiInfo {Title = swaggerOptions.OpenApiTitle, Version = swaggerOptions.OpenApiVersion}); });

            services.AddAuthentication("IdentityBearer")
                    .AddIdentityServerAuthentication("IdentityBearer",
                                                     options =>
                                                     {
                                                         options.Authority = "https://localhost:5005";
                                                         options.RequireHttpsMetadata = false;
                                                         options.RoleClaimType = ClaimTypes.Role;
                                                     });
            
            services.AddFluentMigratorCore()
                    .ConfigureRunner(rb =>
                                     {
                                         ConfigureMigrator(rb);
                                         rb.ScanIn(domainAssemblies).For.Migrations();
                                     })
                    .AddLogging(lb => lb.AddFluentMigratorConsole());

            var bus = RabbitHutch.CreateBus(Configuration.GetConnectionString("RabbitMQ"),
                                            svc => svc.EnableMessageVersioning());
            var rabbitMessageBus = new RabbitMqMessageBus(bus);
            services.AddSingleton<IMessageBus>(rabbitMessageBus);
            services.AddSingleton<RabbitScopedMessageDispatcher>();
            services.AddSingleton(p => new AutoSubscriber(rabbitMessageBus.Bus, MicroserviceName)
                                       {
                                           AutoSubscriberMessageDispatcher = new RabbitScopedMessageDispatcher(p)
                                       });

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                
                var swaggerOptions = new SwaggerOptions();
                ConfigureSwagger(swaggerOptions);
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{swaggerOptions.OpenApiTitle} {swaggerOptions.OpenApiVersion}"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.ApplicationServices
               .GetService<AutoSubscriber>()
               .Subscribe(AppDomain.CurrentDomain.GetAssemblies());
            
            using var scope = app.ApplicationServices.CreateScope();
            var migrationsRunner = scope.ServiceProvider.GetService<IMigrationRunner>();
            if (migrationsRunner!.HasMigrationsToApplyUp())
            {
                migrationsRunner.MigrateUp();
            }
            
            RegisterApp(app, env);
        }

        private static IEnumerable<Assembly> LoadAllDomainAssemblies()
        {
            var returnAssemblies = new List<Assembly>();
            var loadedAssemblies = new HashSet<string>();
            var assembliesToCheck = new Queue<Assembly>();

            var entryAssembly = Assembly.GetEntryAssembly();
            returnAssemblies.Add(entryAssembly);
            
            assembliesToCheck.Enqueue(entryAssembly);

            while (assembliesToCheck.Any())
            {
                var assemblyToCheck = assembliesToCheck.Dequeue();

                foreach (var reference in assemblyToCheck.GetReferencedAssemblies()
                                                         .Where(o => o.Name!.StartsWith(nameof(PopugJira))))
                {
                    if (!loadedAssemblies.Contains(reference.FullName))
                    {
                        var assembly = Assembly.Load(reference);
                        assembliesToCheck.Enqueue(assembly);
                        loadedAssemblies.Add(reference.FullName);
                        returnAssemblies.Add(assembly);
                    }
                }
            }

            return returnAssemblies;
        }
    }
}