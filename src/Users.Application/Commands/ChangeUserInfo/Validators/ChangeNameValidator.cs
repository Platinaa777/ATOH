using FluentValidation;
using Users.Application.Commands.ChangeUserInfo.Requests;
using Users.Application.RegexUtils;

namespace Users.Application.Commands.ChangeUserInfo.Validators;

public class ChangeNameValidator : AbstractValidator<ChangeName>
{
    public ChangeNameValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .WithMessage("Login is required.")
            .Matches(RegexConstants.CredentialRegex)
            .WithMessage("Login can only contain Latin and Cyrillic letters.");
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .Matches(RegexConstants.NameRegex)
            .WithMessage("Name can only contain Latin and Cyrillic letters.");
    }
}