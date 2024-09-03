using MediatR;
using System.ComponentModel;

namespace IdentityService.Application.Features.Auth.Command.Login
{
    public class LoginCommandRequest : IRequest<LoginCommandResponse>
    {
        [DefaultValue("hakkican@sozerbilgisayar.com")]
        public string Email { get; set; }
        [DefaultValue("wQAvf4!Qz2~'")]
        public string Password { get; set; }
    }
}
