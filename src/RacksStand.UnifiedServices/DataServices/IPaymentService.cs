
namespace RacksStand.UnifiedServices.DataServices;

public interface IPaymentService
{
    Task<Payment> ProcessPaymentAsync(PaymentModel paymentModel, CancellationToken cancellationToken = default);
}