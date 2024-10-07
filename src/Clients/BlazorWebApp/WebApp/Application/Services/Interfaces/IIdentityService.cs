namespace WebApp.Application.Services.Interfaces
{
    public interface IIdentityService
    {
        string? GetUserName();
        string? GetUserToken();
        Guid? GetUserId();
        bool IsLoggedIn { get; }
        Task<bool> Login(string email, string password);
        void Logout();
    }
}
