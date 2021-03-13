#region Usings

using System;
using System.Threading.Tasks;
using Blazored.Modal;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using PopugJira.Services;

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

            services.TryAddSingleton<CurrentUserIdentity>();
            services.TryAddTransient<AuthorizationFailedHandler>();

            var httpClientBuilder = services.AddHttpClient("goal_tracker",
                                                           client =>
                                                           {
                                                               client.BaseAddress = new Uri(builder.Configuration["BaseUrls:GoalTracker"]);
                                                           })
                                            .AddHttpMessageHandler<AuthorizationFailedHandler>();

            services.Configure<HttpClientFactoryOptions>(httpClientBuilder.Name,
                                                         options =>
                                                         {
                                                             options.SuppressHandlerScope = true;
                                                             options.HttpClientActions.Add(client =>
                                                                                           {
                                                                                               client.SetBearerToken(TokensContainer.GoalTrackerToken);
                                                                                           });
                                                         });
        }
    }
}