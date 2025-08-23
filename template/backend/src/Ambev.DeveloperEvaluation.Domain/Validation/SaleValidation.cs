using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for <see cref="Sale"/> entity.
/// Ensures that sales comply with business rules and required fields.
/// </summary>
public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(s => s.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required.");

        RuleFor(s => s.CustomerName)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(200).WithMessage("Customer name must not exceed 200 characters.");

        RuleFor(s => s.BranchId)
            .NotEmpty().WithMessage("BranchId is required.");

        RuleFor(s => s.BranchName)
            .NotEmpty().WithMessage("Branch name is required.")
            .MaximumLength(200).WithMessage("Branch name must not exceed 200 characters.");

        RuleFor(s => s.Items)
            .NotEmpty().WithMessage("A sale must contain at least one item.");

        RuleForEach(s => s.Items)
            .SetValidator(new SaleItemValidator());
    }
}
