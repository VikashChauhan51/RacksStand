using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    [Alias("Racks")]
    public  class Rack
    {
        public Rack()
        {
            this.Boxes = new List<RackBox>();
        }

        public string Id { get; set; }
        public string RoomId { get; set; }
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Status { get; set; }
        public int? Index { get; set; }
        public byte SecondaryStatus { get; set; }
        public int BoxCapacity { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        [Reference]
        public List<RackBox> Boxes { get; set; }
    }
}
