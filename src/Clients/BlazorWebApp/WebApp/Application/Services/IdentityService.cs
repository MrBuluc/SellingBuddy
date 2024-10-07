using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using WebApp.Application.Services.DTOs;
using WebApp.Application.Services.Interfaces;
using WebApp.Domain.Models.User;
using WebApp.Extensions;
using WebApp.Utils;

namespace WebApp.Application.Services
{
    public class IdentityService(HttpClient httpClient, ISyncLocalStorageService syncLocalStorageService, AuthenticationStateProvider authStateProvider) : IIdentityService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly ISyncLocalStorageService syncLocalStorageService = syncLocalStorageService;
        private readonly AuthenticationStateProvider authStateProvider = authStateProvider;

        public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());

        public string? GetUserName() => syncLocalStorageService.GetUsername();

        public string? GetUserToken() => syncLocalStorageService.GetToken();

        public Guid? GetUserId() => syncLocalStorageService.GetUserId();

        public async Task<bool> Login(string email, string password)
        {
            MyHttpResponseMessage response = await httpClient.PostGetResponseAsync<UserLoginResponse, UserLoginRequest>("auth/Login", new()
            {
                Email = email,
                Password = password
            });

            if (!response.IsSuccessStatusCode && response.Content is not null)
            {
                throw response.Content;
            }
            if (!response.IsSuccessStatusCode || response.Content is null)
            {
                return false;
            }

            UserLoginResponse userLoginResponse = (response.Content as UserLoginResponse)!;
            syncLocalStorageService.SetToken(userLoginResponse.Token);
            syncLocalStorageService.SetUsername(userLoginResponse.Name);
            syncLocalStorageService.SetUserId(userLoginResponse.Id);

            ((AuthStateProvider)authStateProvider).NotifyUserLogin(userLoginResponse.Name);

            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", userLoginResponse.Token);

            return true;
        }

        public void Logout()
        {
            syncLocalStorageService.RemoveItem("token");
            syncLocalStorageService.RemoveItem("username");
            syncLocalStorageService.RemoveItem("userId");

            ((AuthStateProvider)authStateProvider).NotifyUserLogout();

            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
