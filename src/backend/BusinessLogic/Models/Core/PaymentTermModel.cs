using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Core
{
    [Serializable]
    public class PaymentTermModel
    {
        [Required]
        [MaxLength(32)]
        public string Id { get; set; }
        public string CompanyId { get; set; }
        [Required]
        [MaxLength(10)]
        public string Code { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [Range(0,Int32.MaxValue)]
        public int NumberOfDays { get; set; }
    }
}
