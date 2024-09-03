using Microsoft.AspNetCore.Identity;

namespace IdentityService.Application.Exceptions
{
    public class UserManagerCreateException(IEnumerable<IdentityError> identityErrors) : Exception(string.Join("; ", identityErrors.Select(iE => iE.Description))) { }
}
