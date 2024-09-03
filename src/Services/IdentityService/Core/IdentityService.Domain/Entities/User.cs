using IdentityService.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Domain.Entities
{
    public class User : IdentityUser<Guid>, IEntityBase
    {
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
