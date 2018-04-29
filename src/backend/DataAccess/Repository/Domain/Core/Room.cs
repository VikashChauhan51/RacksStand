using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    [Alias("Rooms")]
    public class Room
    {
        public Room()
        {
            this.Racks = new List<Rack>();
        }
        public string Id { get; set; }
        public string StoreId { get; set; }
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Status { get; set; }
        public int? Index { get; set; }
        public byte SecondaryStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        [Reference]
        public List<Rack> Racks { get; set; }
    }
}
