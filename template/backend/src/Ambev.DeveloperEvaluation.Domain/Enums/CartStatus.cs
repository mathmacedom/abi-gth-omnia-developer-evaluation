namespace Ambev.DeveloperEvaluation.Domain.Enums;

/// <summary>
/// Represents the lifecycle status of a cart.
/// </summary>
public enum CartStatus
{
    Active = 1,
    Converted = 2,
    Abandoned = 3,
    Expired = 4,
    PendingPayment = 5,
    Cancelled = 6
}