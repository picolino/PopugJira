#region Usings

using System;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazored.Modal;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
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
            
            await ConfigureServices(builder.Configuration, builder.Services);
            
            var provider = builder.Services.BuildServiceProvider();
            
            await Configure(builder, provider);
            
            await builder.Build().RunAsync();
        }

        private static Task ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddBlazoredModal();
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();

            services.TryAddScoped<AuthenticationStateProvider, OAuthAuthenticationStateProvider>();
            services.TryAddScoped<AuthService>();
            services.TryAddTransient<AuthorizationFailedHandler>();
            
            services.ConfigureHttpClient("goal_tracker", configuration["BaseUrls:GoalTracker"]);
            services.ConfigureHttpClient("accounting", configuration["BaseUrls:Accounting"]);
            services.ConfigureHttpClient("analytics", configuration["BaseUrls:Analytics"]);

            return Task.CompletedTask;
        }

        private static Task Configure(WebAssemblyHostBuilder builder, IServiceProvider provider)
        {
            return Task.CompletedTask;
        }
    }

    public static class ServiceProviderExtensions
    {
        public static void ConfigureHttpClient(this IServiceCollection services, string httpClientName, string apiBaseUrl)
        {
            var builder = services.AddHttpClient(httpClientName, client => { client.BaseAddress = new Uri(apiBaseUrl); })
                                  .AddHttpMessageHandler<AuthorizationFailedHandler>();
            
            services.Configure<HttpClientFactoryOptions>(builder.Name,
                                                         options =>
                                                         {
                                                             var provider = services.BuildServiceProvider();
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