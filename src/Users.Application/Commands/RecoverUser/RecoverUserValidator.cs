using FluentValidation;
using Users.Application.RegexUtils;

namespace Users.Application.Commands.RecoverUser;

public class RecoverUserValidator : AbstractValidator<RecoverUser>
{
    public RecoverUserValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .WithMessage("Login is required.")
            .Matches(RegexConstants.CredentialRegex)
            .WithMessage("Login can only contain Latin and Cyrillic letters.");
    }
}