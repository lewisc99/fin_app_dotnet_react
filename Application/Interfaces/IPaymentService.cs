using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPaymentService
    {
        public Task<PaymentResponse> ProcessPaymentAsync(CreatePaymentRequest request);
    }
}
