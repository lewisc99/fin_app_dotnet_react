using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly Appd
        public Task AddAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Payment?> GetByIdAsync(PaymentId id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
