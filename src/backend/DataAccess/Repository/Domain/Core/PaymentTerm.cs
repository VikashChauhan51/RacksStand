using ServiceStack.DataAnnotations;
namespace Domain.Core
{
    [Alias("PaymentTerms")]
    public class PaymentTerm
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int NumberOfDays { get; set; }
    }
}
