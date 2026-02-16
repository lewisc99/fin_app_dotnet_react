namespace Domain.ValueObjects;

public readonly record struct PaymentId(Guid value)
{
    public static PaymentId New() => new(Guid.NewGuid());
    public static PaymentId Empty => new(Guid.Empty);
}
