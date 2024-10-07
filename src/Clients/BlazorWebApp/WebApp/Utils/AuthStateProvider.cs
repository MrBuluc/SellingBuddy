using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WebApp.Extensions;
using WebApp.Infrastructure;

namespace WebApp.Utils
{
    public class AuthStateProvider(ILocalStorageService localStorageService, HttpClient httpClient, AppStateManager appState) : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService = localStorageService;
        private readonly HttpClient httpClient = httpClient;
        private readonly AppStateManager appState = appState;
        private readonly AuthenticationState anonymous = new(new ClaimsPrincipal(new ClaimsIdentity()));

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string? apiToken = await localStorageService.GetToken(), username = await localStorageService.GetUsername();


            if (string.IsNullOrEmpty(apiToken) || string.IsNullOrEmpty(username))
            {
                return anonymous;
            }

            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", apiToken);

            return new(new(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            }, "jwtAuthType")));
        }

        public void NotifyUserLogin(string username)
        {
            NotifyAuthenticationStateChanged(Task.FromResult<AuthenticationState>(new(new(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            }, "jwtAuthType")))));

            appState.LoginChanged(null);
        }

        public void NotifyUserLogout()
        {
            NotifyAuthenticationStateChanged(Task.FromResult<AuthenticationState>(anonymous));
            appState.LoginChanged(null);
        }
    }
}
