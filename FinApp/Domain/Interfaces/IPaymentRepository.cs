using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetByIdAsync(PaymentId id, CancellationToken cancellationToken = default(CancellationToken));

        Task AddAsync(Payment payment, CancellationToken cancellationToken= default(CancellationToken));
    }
}
