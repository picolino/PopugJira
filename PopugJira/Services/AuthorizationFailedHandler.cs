using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace PopugJira.Services
{
    public class AuthorizationFailedHandler : DelegatingHandler
    {
        private readonly NavigationManager navigationManager;

        public AuthorizationFailedHandler(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var content = await response.Content.ReadAsStringAsync(cancellationToken);
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        navigationManager.NavigateTo("/login", true);
                    }
                }
            }

            return response;
        }
    }
}