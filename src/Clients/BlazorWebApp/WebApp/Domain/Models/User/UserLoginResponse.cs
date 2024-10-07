namespace WebApp.Domain.Models.User
{
    public class UserLoginResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}
