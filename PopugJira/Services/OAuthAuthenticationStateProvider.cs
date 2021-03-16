using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components.Authorization;

namespace PopugJira.Services
{
    public class OAuthAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;

        public OAuthAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedAuthInfo = await localStorage.GetItemAsync<JsonElement>("auth");
            var savedAccessToken = savedAuthInfo.TryGetString("access_token");
            
            if (string.IsNullOrWhiteSpace(savedAccessToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            
            var savedUserInfo = await localStorage.GetItemAsync<JsonElement>("u");
            var claims = savedUserInfo.ToClaims();
            
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "password")));
        }

        public void MarkUserAsAuthenticated(IEnumerable<Claim> claims)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}