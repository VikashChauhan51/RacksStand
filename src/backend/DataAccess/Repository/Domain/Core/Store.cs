using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    [Alias("Stores")]
    public  class Store
    {
        public Store()
        {
            this.Rooms = new List<Room>();
        }
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public byte Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Description { get; set; }
        public byte Status { get; set; }
        public int? Index { get; set; }
        public byte SecondaryStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        [Reference]
        public List<Room> Rooms { get; set; }
    }
}
