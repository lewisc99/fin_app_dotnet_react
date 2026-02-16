using Domain.Exceptions;

public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; } 

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money From(decimal amount, string currency)
    {
        if (amount < 0) throw new DomainException("Money cannot be negative.");
        if (string.IsNullOrEmpty(currency) || currency.Length != 3)
            throw new DomainException("Invalid currency code.");

        return new Money(amount, currency.ToUpper());
    }

    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency) throw new DomainException("Currency mismatch");

        return new Money(a.Amount + b.Amount, a.Currency);
    }

}