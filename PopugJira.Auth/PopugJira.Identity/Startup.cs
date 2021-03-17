using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PopugJira.EventBus;
using PopugJira.Identity.Data;

namespace PopugJira.Identity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            
            services.AddDbContext<ApplicationDbContext>(options =>
                                                            options.UseSqlite(Configuration.GetConnectionString("SQLite"),
                                                                              o => o.MigrationsAssembly(migrationsAssembly)));

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryIdentityResources(Config.GetIdentityResources())
                    .AddInMemoryApiScopes(Config.GetApiScopes())
                    .AddInMemoryApiResources(Config.GetApiResources())
                    .AddInMemoryClients(Config.GetClients())
                    .AddAspNetIdentity<IdentityUser>();

            services.Configure<IdentityOptions>(options =>
                                                {
                                                    options.SignIn.RequireConfirmedAccount = false;
                                                    options.SignIn.RequireConfirmedEmail = false;
                                                    options.SignIn.RequireConfirmedPhoneNumber = false;

                                                    options.Password.RequireDigit = false;
                                                    options.Password.RequiredLength = 4;
                                                    options.Password.RequireLowercase = false;
                                                    options.Password.RequireUppercase = false;
                                                    options.Password.RequiredUniqueChars = 0;
                                                    options.Password.RequireNonAlphanumeric = false;

                                                    options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
                                                    options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
                                                });

            services.AddControllers();

            var rabbitMessageBus = new RabbitMqMessageBus(RabbitHutch.CreateBus(Configuration.GetConnectionString("RabbitMQ")));
            services.AddSingleton<IMessageBus>(rabbitMessageBus);

            CreateRoles(services).Wait();
        }

        private static async Task CreateRoles(IServiceCollection serviceCollection)
        {
            await using var sp = serviceCollection.BuildServiceProvider();
            var roleManager = sp.GetService<RoleManager<IdentityRole>>();
                                   
            var roles = new[]
                        {
                            "admin",
                            "programmer",
                            "bookkeeper",
                            "manager"
                        };

            foreach (var roleName in roles)
            {
                if (roleManager != null && !await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseIdentityServer();
            
            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapDefaultControllerRoute();
                             });
        }
    }
}