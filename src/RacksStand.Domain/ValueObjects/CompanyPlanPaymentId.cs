using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record CompanyPlanPaymentId
{
    public string Value { get; }
    private CompanyPlanPaymentId(string value) => Value = value;
    public static CompanyPlanPaymentId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CompanyPlanPaymentId cannot be null or whitespace.");
        }

        return new CompanyPlanPaymentId(value);
    }
}
