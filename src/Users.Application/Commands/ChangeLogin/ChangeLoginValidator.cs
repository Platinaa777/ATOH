using FluentValidation;
using Users.Application.RegexUtils;

namespace Users.Application.Commands.ChangeLogin;

public class ChangeLoginValidator : AbstractValidator<ChangeLogin>
{
    public ChangeLoginValidator()
    {
        RuleFor(x => x.CurrentLogin)
            .NotEmpty()
            .WithMessage("Login is required.")
            .Matches(RegexConstants.CredentialRegex)
            .WithMessage("Login can only contain Latin and Cyrillic letters.");
        
        RuleFor(x => x.NewLogin)
            .NotEmpty()
            .WithMessage("NewLogin is required.")
            .Matches(RegexConstants.CredentialRegex)
            .WithMessage("NewLogin can only contain Latin and Cyrillic letters.");
    }
}