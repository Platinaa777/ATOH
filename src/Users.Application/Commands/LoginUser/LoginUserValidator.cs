using FluentValidation;
using Users.Application.RegexUtils;

namespace Users.Application.Commands.LoginUser;

public class LoginUserValidator : AbstractValidator<LoginUser>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .WithMessage("Login is required.")
            .Matches(RegexConstants.CredentialRegex)
            .WithMessage("Login can only contain Latin and Cyrillic letters.");
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .Matches(RegexConstants.CredentialRegex)
            .WithMessage("Password can only contain Latin and Cyrillic letters.");
    }
}