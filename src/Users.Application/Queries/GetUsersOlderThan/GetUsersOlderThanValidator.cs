using FluentValidation;
using MediatR;

namespace Users.Application.Queries.GetUsersOlderThan;

public class GetUsersOlderThanValidator : AbstractValidator<GetUsersOlderThan>
{
    public GetUsersOlderThanValidator()
    {
        RuleFor(x => x.Age)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Age should be positive");
    }
}