using FluentValidation;
using Users.Application.Commands.ChangeUserInfo.Requests;
using Users.Application.RegexUtils;

namespace Users.Application.Commands.ChangeUserInfo.Validators;

public class ChangeGenderValidator : AbstractValidator<ChangeGender>
{
    public ChangeGenderValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .WithMessage("Login is required.")
            .Matches(RegexConstants.CredentialRegex)
            .WithMessage("Login can only contain Latin and Cyrillic letters.");
        
        RuleFor(x => x.Gender)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Gender id type should be greater than 0");
    }
}