#region Usings

using System;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazored.Modal;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components.Authorization;
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
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();

            services.TryAddScoped<AuthenticationStateProvider, OAuthAuthenticationStateProvider>();
            services.TryAddScoped<AuthService>();
            services.TryAddTransient<AuthorizationFailedHandler>();

            var provider = services.BuildServiceProvider();

            var httpClientBuilder = services.AddHttpClient("goal_tracker",
                                                           client => { client.BaseAddress = new Uri(builder.Configuration["BaseUrls:GoalTracker"]); })
                                            .AddHttpMessageHandler<AuthorizationFailedHandler>();

            services.Configure<HttpClientFactoryOptions>(httpClientBuilder.Name,
                                                         options =>
                                                         {
                                                             options.SuppressHandlerScope = true;
                                                             options.HttpClientActions.Add(client =>
                                                                                           {
                                                                                               var localStorage = provider.GetService<ISyncLocalStorageService>();
                                                                                               if (localStorage is not null)
                                                                                               {
                                                                                                   var savedAuthInfo = localStorage.GetItem<JsonElement>("auth");
                                                                                                   var savedAccessToken = savedAuthInfo.TryGetString("access_token");
                                                                                                   client.SetBearerToken(savedAccessToken);
                                                                                               }
                                                                                           });
                                                         });
        }
    }
}