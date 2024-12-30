using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Models;

namespace RacksStand.Api.Core.Infrastructure.Services;

public interface IPaymentService
{
    Task<Payment> ProcessPaymentAsync(PaymentModel paymentModel, CancellationToken cancellationToken = default);
}
