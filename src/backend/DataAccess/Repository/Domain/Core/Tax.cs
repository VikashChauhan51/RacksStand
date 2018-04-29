using ServiceStack.DataAnnotations;
namespace Domain.Core
{
    [Alias("Taxes")]
    public  class Tax
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Rate { get; set; }
        public bool IsCompound { get; set; }
        public bool IsActive { get; set; }
    }
}
