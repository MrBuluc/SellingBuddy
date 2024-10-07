using Blazored.LocalStorage;

namespace WebApp.Extensions
{
    public static class LocalStorageExtension
    {
        public static string? GetUsername(this ISyncLocalStorageService localStorageService) => localStorageService.GetItem<string?>("username");

        public static async Task<string?> GetUsername(this ILocalStorageService localStorageService) => await localStorageService.GetItemAsync<string?>("username");

        public static void SetUsername(this ISyncLocalStorageService localStorageService, string username)
        {
            localStorageService.SetItem("username", username);
        }

        public static string? GetToken(this ISyncLocalStorageService localStorageService) => localStorageService.GetItem<string?>("token");

        public async static Task<string?> GetToken(this ILocalStorageService localStorageService) => await localStorageService.GetItemAsync<string?>("token");

        public static void SetToken(this ISyncLocalStorageService localStorageService, string token)
        {
            localStorageService.SetItem("token", token);
        }

        public static void SetUserId(this ISyncLocalStorageService localStorageService, Guid userId)
        {
            localStorageService.SetItem("userId", userId);
        }

        public static Guid? GetUserId(this ISyncLocalStorageService localStorageService) => localStorageService.GetItem<Guid?>("userId");
    }
}
