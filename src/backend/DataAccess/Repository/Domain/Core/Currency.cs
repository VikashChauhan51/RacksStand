using ServiceStack.DataAnnotations;
namespace Domain.Core
{
    [Alias("Currencies")]
    public class Currency
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double ExchangeRate { get; set; }
        public bool IsActive { get; set; }
    }
}
