#region Usings

using System;
using System.Threading.Tasks;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace PopugJira
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            ConfigureServices(builder, builder.Services);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(WebAssemblyHostBuilder builder, IServiceCollection services)
        {
            services.AddBlazoredModal();
            services.AddHttpClient("goal_tracker",
                                   c => { c.BaseAddress = new Uri(builder.Configuration["BaseUrls:GoalTracker"]); });
        }
    }
}