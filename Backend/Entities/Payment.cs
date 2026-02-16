using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;
public class Payment : BaseEntity<PaymentId>
{
    public Money Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public PaymentMethod Method { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    //private readonly List<PaymentTag> _tags = new();


}
