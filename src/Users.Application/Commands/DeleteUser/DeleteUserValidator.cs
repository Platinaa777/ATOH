using FluentValidation;
using Users.Application.RegexUtils;

namespace Users.Application.Commands.DeleteUser;

public class DeleteUserValidator
    : AbstractValidator<DeleteUser>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .WithMessage("Login is required.")
            .Matches(RegexConstants.OnlyLatinAndCyrillicLetters)
            .WithMessage("Login can only contain Latin and Cyrillic letters.");
    }
}