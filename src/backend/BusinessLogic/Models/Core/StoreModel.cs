using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Core
{
    [Serializable]
    public class StoreModel
    {
        public StoreModel()
        {
            this.Rooms = new List<RoomModel>();
        }
        [Required]
        [MaxLength(32)]
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(150)]
        public string Street { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public string CompanyId { get; set; }
        [Required]
        [Range(0, 255)]
        public byte Country { get; set; }
        [Required]
        [MaxLength(50)]
        public string State { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Fax { get; set; }
        [Phone]
        public string Phone { get; set; }
        [Required]
        [MaxLength(20)]
        public string ZipCode { get; set; }
        public int? Index { get; set; }
        public byte SecondaryStatus { get; set; }
        public byte Status { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public ICollection<RoomModel> Rooms { get; set; }
    }
}
