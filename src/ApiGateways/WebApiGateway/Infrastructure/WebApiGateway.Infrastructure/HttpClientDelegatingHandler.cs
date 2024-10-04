using Microsoft.AspNetCore.Http;

namespace WebApiGateway.Infrastructure
{
    public class HttpClientDelegatingHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
    {
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Microsoft.Extensions.Primitives.StringValues authorization = httpContextAccessor.HttpContext.Request.Headers.Authorization;
            if (!string.IsNullOrEmpty(authorization))
            {
                if (request.Headers.Contains("Authorization"))
                {
                    request.Headers.Remove("Authorization");
                }

                request.Headers.Add("Authorization", new List<string> { authorization! });
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
