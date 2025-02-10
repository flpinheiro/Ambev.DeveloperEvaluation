using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class PaginatedRequestValidator : AbstractValidator<PaginatedRequest>
{
    public PaginatedRequestValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("Must be greater than 0");
        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Must be Greate than 0");
    }
}
