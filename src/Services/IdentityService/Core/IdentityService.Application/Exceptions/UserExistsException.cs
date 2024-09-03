namespace IdentityService.Application.Exceptions
{
    public class UserExistsException() : Exception("This email is registered!") { }
}
