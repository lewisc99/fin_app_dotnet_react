using Domain.Common;

namespace Domain.ValueObjects;

public record PaymentApprovedEvent(PaymentId PaymentId, Money Amount) : IDomainEvent;