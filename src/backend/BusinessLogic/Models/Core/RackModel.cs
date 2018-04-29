using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Core
{
    [Serializable]
    public class RackModel
    {
        public RackModel()
        {
            this.Boxes = new List<RackBoxModel>();
        }
        [Required]
        [MaxLength(32)]
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [Required]
        [MaxLength(32)]
        public string RoomId { get; set; }
        [Required]
        [Range(0, 255)]
        public int Rows { get; set; }
        [Required]
        [Range(0, 255)]
        public int Columns { get; set; }
        [Required]
        public int BoxCapacity { get; set; }
        public byte Status { get; set; }
        public int? Index { get; set; }
        public byte SecondaryStatus { get; set; }
        public string CompanyId { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public ICollection<RackBoxModel> Boxes { get; set; }
    }
}
