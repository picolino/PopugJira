#region Usings

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.Modal;
using IdentityModel.Client;
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

            await ConfigureServices(builder, builder.Services);

            await builder.Build().RunAsync();
        }

        private static async Task ConfigureServices(WebAssemblyHostBuilder builder, IServiceCollection services)
        {
            services.AddBlazoredModal();

            var client = new TokenClient(new HttpClient(),
                                         new TokenClientOptions
                                         {
                                             Address = "https://localhost:5005/connect/token",
                                             ClientId = "client",
                                             ClientSecret = "SECRET"
                                         });
            
            var token = await client.RequestClientCredentialsTokenAsync();
            
            services.AddHttpClient("goal_tracker",
                                   c =>
                                   {
                                       c.BaseAddress = new Uri(builder.Configuration["BaseUrls:GoalTracker"]);
                                       c.SetBearerToken(token.AccessToken);
                                   });
        }
    }
}