using FluentValidation;
using MediatR;

namespace Users.Application.Queries.GetUsersOlderThan;

public class GetUsersOlderThanValidator : AbstractValidator<GetUsersOlderThan>
{
    public GetUsersOlderThanValidator()
    {
        RuleFor(x => x.Age)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Age should be positive");
    }
}