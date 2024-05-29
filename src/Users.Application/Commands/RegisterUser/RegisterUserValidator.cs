using FluentValidation;
using Users.Application.RegexUtils;

namespace Users.Application.Commands.RegisterUser;

public class RegisterUserValidator
    : AbstractValidator<RegisterUser>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .WithMessage("Login is required.")
            .Matches(RegexConstants.OnlyLatinAndCyrillicLetters)
            .WithMessage("Login can only contain Latin and Cyrillic letters.");
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .Matches(RegexConstants.OnlyLatinAndCyrillicLetters)
            .WithMessage("Password can only contain Latin and Cyrillic letters.");
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .Matches(RegexConstants.OnlyLatinAndCyrillicLetters)
            .WithMessage("Name can only contain Latin and Cyrillic letters.");

        RuleFor(x => x.Gender)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Gender id type should be greater than 0");
        
        // можно конечно сделать валидацию на возраст, но в задании не сказано про это
        // так что думаю оставлю, что система не может регистрировать пользователей
        // которые родятся в будущем
        RuleFor(x => x.Birthday)
            .LessThan(DateTime.Now)
            .WithMessage("Birthday should be less than current time");
    }
}