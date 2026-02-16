using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities;
public class Payment : BaseEntity<PaymentId>
{
    public Money Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public PaymentMethod Method { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    private readonly List<PaymentTag> _tags = new();
    public IReadOnlyCollection<PaymentTag> Tags => _tags.AsReadOnly();
    private Payment() { }

    public Payment(Money amount, PaymentMethod method)
    {
        Id = new PaymentId(Guid.NewGuid());
        Amount = amount;
        Method = method;
        Status = PaymentStatus.Pending;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void Approve()
    {
        if (Status != PaymentStatus.Pending) throw new DomainException("Only pending payments can be approved");

        Status = PaymentStatus.Approved;
        UpdatedAt = DateTimeOffset.UtcNow;

        AddDomainEvent(new PaymentApprovedEvent(this.Id, this.Amount));
    }

    public void AddTag(Tag tag)
    {
        if (!_tags.Any(t => t.TagId == tag.Id))
            _tags.Add(new PaymentTag(this.Id, tag.Id));
    }
}
