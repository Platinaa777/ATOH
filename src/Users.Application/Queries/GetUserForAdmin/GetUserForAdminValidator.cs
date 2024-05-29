using FluentValidation;
using Users.Application.RegexUtils;

namespace Users.Application.Queries.GetUserForAdmin;

public class GetUserForAdminValidator : AbstractValidator<GetUserForAdmin>
{
    public GetUserForAdminValidator()
    {
        RuleFor(x => x.UserLogin)
            .NotEmpty()
            .WithMessage("Login is required.")
            .Matches(RegexConstants.OnlyLatinAndCyrillicLetters)
            .WithMessage("Login can only contain Latin and Cyrillic letters.");
    }
}