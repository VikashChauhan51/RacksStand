using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Models;

namespace RacksStand.Api.Core.Services;

public interface IPaymentService
{
    Task<Payment> ProcessPaymentAsync(PaymentModel paymentModel, CancellationToken cancellationToken = default);
}
