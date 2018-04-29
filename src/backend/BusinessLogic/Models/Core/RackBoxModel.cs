using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Core
{
    [Serializable]
    public class RackBoxModel
    {
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
        public string RackId { get; set; }
        public int? Index { get; set; }
        public byte SecondaryStatus { get; set; }
        public string CompanyId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int? CurrentSize { get; set; }
    }
}
