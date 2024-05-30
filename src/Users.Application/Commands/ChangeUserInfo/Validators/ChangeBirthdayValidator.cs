using FluentValidation;
using Users.Application.Commands.ChangeUserInfo.Requests;
using Users.Application.RegexUtils;

namespace Users.Application.Commands.ChangeUserInfo.Validators;

public class ChangeBirthdayValidator : AbstractValidator<ChangeBirthday>
{
    public ChangeBirthdayValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .WithMessage("Login is required.")
            .Matches(RegexConstants.CredentialRegex)
            .WithMessage("Login can only contain Latin and Cyrillic letters.");
        
        RuleFor(x => x.Birthday)
            .LessThan(DateTime.UtcNow)
            .WithMessage("Birthday should be less than current time");
    }
}