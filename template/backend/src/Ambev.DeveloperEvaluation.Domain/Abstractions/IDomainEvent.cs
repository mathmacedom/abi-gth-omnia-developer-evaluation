﻿namespace Ambev.DeveloperEvaluation.Domain.Abstractions;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
