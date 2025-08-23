using System.Collections.ObjectModel;

namespace Ambev.DeveloperEvaluation.Domain.Abstractions;

public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    private readonly List<IDomainEvent> _domainEvents = new();
    public ReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void Raise(IDomainEvent @event) => _domainEvents.Add(@event);

    /// <summary>
    /// Retorna e limpa a lista de eventos
    /// </summary>
    
    public IReadOnlyCollection<IDomainEvent> PullDomainEvents()
    {
        var copy = _domainEvents.ToList().AsReadOnly();
        _domainEvents.Clear();
        return copy;
    }
}