using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components.Authorization;
using PopugJira.Models;

namespace PopugJira.Services
{
    public class AuthService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly ILocalStorageService localStorage;

        public AuthService(IHttpClientFactory httpClientFactory,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            this.httpClientFactory = httpClientFactory;
            this.authenticationStateProvider = authenticationStateProvider;
            this.localStorage = localStorage;
        }

        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            var client = httpClientFactory.CreateClient();
            
            var settings = new JsonSerializerOptions();
            settings.Converters.Add(new JsonStringEnumConverter());
            
            var result = await client.PostAsJsonAsync("https://localhost:5005/api/v1/accounts/register", registerModel, settings);
            var content = await result.Content.ReadAsStringAsync();
            
            if (!result.IsSuccessStatusCode)
            {
                return new RegisterResult
                       {
                           IsSuccess = false,
                           Error = content
                       };
            }
            
            return new RegisterResult
                   {
                       IsSuccess = true
                   };
        }

        public async Task<LoginResult> Login(string login, string password)
        {
            var http = httpClientFactory.CreateClient();
            var client = new TokenClient(http,
                                         new TokenClientOptions
                                         {
                                             Address = "https://localhost:5005/connect/token",
                                             ClientId = "blazor-client",
                                             ClientSecret = "SECRET"
                                         });
            
            var token = await client.RequestPasswordTokenAsync(login, password);

            if (token.IsError)
            {
                return new LoginResult
                       {
                           IsSuccess = false,
                           Error = token.Error
                       };
            }
            
            await localStorage.SetItemAsync("auth", token.Json);

            var ui = await http.GetUserInfoAsync(new UserInfoRequest
                                                 {
                                                     Address = "https://localhost:5005/connect/userinfo",
                                                     Token = token.AccessToken
                                                 });

            if (ui.IsError)
            {
                return new LoginResult
                       {
                           IsSuccess = false,
                           Error = ui.Error
                       };
            }

            await localStorage.SetItemAsync("u", ui.Json);
            
            ((OAuthAuthenticationStateProvider) authenticationStateProvider).MarkUserAsAuthenticated(ui.Claims);

            return new LoginResult
                   {
                       IsSuccess = true
                   };
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("auth");
            ((OAuthAuthenticationStateProvider) authenticationStateProvider).MarkUserAsLoggedOut();
        }
    }
}