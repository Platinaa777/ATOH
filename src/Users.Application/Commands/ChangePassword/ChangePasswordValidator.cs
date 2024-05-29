using FluentValidation;
using Users.Application.RegexUtils;

namespace Users.Application.Commands.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<ChangePassword>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.CurrentLogin)
            .NotEmpty()
            .WithMessage("Login is required.")
            .Matches(RegexConstants.OnlyLatinAndCyrillicLetters)
            .WithMessage("Login can only contain Latin and Cyrillic letters.");
        
        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage("Password is required.")
            .Matches(RegexConstants.OnlyLatinAndCyrillicLetters)
            .WithMessage("Password can only contain Latin and Cyrillic letters.");
    }
}