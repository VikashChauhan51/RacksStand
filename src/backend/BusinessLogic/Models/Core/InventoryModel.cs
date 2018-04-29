using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Core
{
    public class InventoryModel
    {
        public InventoryModel()
        {

        }
        [Required]
        [MaxLength(32)]
        public string Id { get; set; }
        [MaxLength(32)]
        public string RackBoxId { get; set; }
        [MaxLength(32)]
        public string CompanyId { get; set; }
        [Required]
        [MaxLength(32)]
        public string CustomerId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [MaxLength(32)]
        public string AccessCode { get; set; }
        public int? Index { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
