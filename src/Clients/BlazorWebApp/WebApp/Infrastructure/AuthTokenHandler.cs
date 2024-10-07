using Blazored.LocalStorage;
using WebApp.Extensions;

namespace WebApp.Infrastructure
{
    public class AuthTokenHandler(ISyncLocalStorageService identityService) : DelegatingHandler
    {
        private readonly ISyncLocalStorageService storageService = identityService;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (storageService is not null)
            {
                request.Headers.Authorization = new("bearer", storageService.GetToken());
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
