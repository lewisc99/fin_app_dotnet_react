using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public class BaseEntity<TId> : IEquatable<BaseEntity<TId>>
{
    public TId Id { get; protected set; }

    private readonly List<IDomainEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public bool Equals(BaseEntity<TId>? other)
    {
        return other is not null && EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object? obj)
    {
        return obj is BaseEntity<TId> entity && Equals(entity);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(Id);
    }

    public static bool operator ==(BaseEntity<TId> left, BaseEntity<TId> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(BaseEntity<TId> left, BaseEntity<TId> right)
    {
        return !Equals(left, right);
    }
}

