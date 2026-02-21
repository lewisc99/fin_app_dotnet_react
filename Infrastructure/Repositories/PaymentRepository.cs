using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;
        public async Task AddAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            await _context.Payments.AddAsync(payment, cancellationToken);
        }

        public async Task<Payment?> GetByIdAsync(PaymentId id, CancellationToken cancellationToken = default)
        {
            return await _context.Payments
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
    }
}
