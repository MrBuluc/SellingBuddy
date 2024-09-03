using FluentValidation;

namespace IdentityService.Application.Features.Users.Command.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email must be not empty.").EmailAddress().WithMessage("Email must be valid.");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be not empty.").MaximumLength(50).WithMessage("Name must be max 50 chars.").MinimumLength(2).WithMessage("Name must be at least 2 chars.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password must be not empty.").MinimumLength(8).WithMessage("Password must be minimum 8 char.").Matches("[0-9]").WithMessage("Password must contain at least one number.").Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.").Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.").Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special char.");
        }
    }
}
