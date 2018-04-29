using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Core
{
    [Serializable]
    public class SupplierModel
    {
        public SupplierModel()
        {
            this.Addresses = new List<SupplierAddressModel>();
        }
        [Required]
        [MaxLength(32)]
        public string Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string MiddleName { get; set; }
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(250)]
        public string Bio { get; set; }
        [MaxLength(50)]
        public string Company { get; set; }
        public string CompanyId { get; set; }
        [MaxLength(8)]
        public string Title { get; set; }
        [MaxLength(20)]
        public string Fax { get; set; }
        [MaxLength(20)]
        public string Mobile { get; set; }
        [MaxLength(20)]
        public string PanNo { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(100)]
        [Url]
        public string Website { get; set; }
        public byte Status { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public ICollection<SupplierAddressModel> Addresses { get; set; }
    }
    [Serializable]
    public class SupplierAddressModel : AddressModel
    {
        public string SupplierId { get; set; }
    }
}
