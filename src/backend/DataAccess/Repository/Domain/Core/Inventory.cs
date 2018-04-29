using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    [Alias("Inventory")]
    public class Inventory
    {
        public string Id { get; set; }
        public string RackBoxId { get; set; }
        public string CompanyId { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AccessCode { get; set; }
        public int? Index { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
