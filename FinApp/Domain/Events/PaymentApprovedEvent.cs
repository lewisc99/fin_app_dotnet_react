using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Events;

public record PaymentApprovedEvent(PaymentId PaymentId, Money Amount) : IDomainEvent;