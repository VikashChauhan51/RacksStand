using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.Enums;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Repositories;
using RacksStand.Models;

namespace RacksStand.Api.Core.Services;
public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService
    (
        IPaymentRepository paymentRepository,
        IUnitOfWork unitOfWork,
        ILogger<PaymentService> logger
    )
    {
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Payment> ProcessPaymentAsync(PaymentModel paymentModel, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding payment details for user {UserId}", paymentModel.UserId);

        var payment = new Payment
        {
            Id = PaymentId.Of(Guid.NewGuid().ToString()),
            UserId = paymentModel.UserId,
            PlanId = PlanId.Of(paymentModel.PlanId),
            Amount = PaymentAmount.Of(paymentModel.Amount),
            PaymentMethod = Enum.Parse<PaymentMethod>(paymentModel.PaymentMethod),
            PaymentDate = paymentModel.PaymentDate,
            TransactionId = paymentModel.TransactionId != null ? TransactionId.Of(paymentModel.TransactionId) : null,
            Status = Enum.Parse<PaymentState>(paymentModel.Status),
            FailureReason = paymentModel.FailureReason,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        await _paymentRepository.AddAsync(payment, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        return payment;
    }
}
