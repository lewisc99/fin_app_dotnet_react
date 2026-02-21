namespace Application.DTOs;

public record CreatePaymentRequest(decimal Amount, string Currency, string Method);
public record PaymentResponse(Guid Id, decimal Amount, string Status, DateTimeOffset CreatedAt);