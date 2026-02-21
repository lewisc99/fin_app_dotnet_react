using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PaymentService> _logger;


        public PaymentService(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork, ILogger<PaymentService> logger)
        {
            _repository = paymentRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<PaymentResponse> ProcessPaymentAsync(CreatePaymentRequest request)
        {
            _logger.LogInformation("Initiating payment for {Amount} {Currency}", request.Amount, request.Currency);

            var money = Money.From(request.Amount, request.Currency);

            if (!Enum.TryParse<PaymentMethod>(request.Method, true, out var method))
            {
                throw new ArgumentException("Invalid payment method");
            }

            var payment = new Payment(money, method);

            if (method == PaymentMethod.CreditCard)
            {
                payment.Approve();
            }


            await _repository.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Payment {Id} processed successfully.", payment.Id.Value);

            // 4. Mapping to DTO
            return new PaymentResponse(
                payment.Id.Value,
                payment.Amount.Amount,
                payment.Status.ToString(),
                payment.CreatedAt);
        }
    }
}
