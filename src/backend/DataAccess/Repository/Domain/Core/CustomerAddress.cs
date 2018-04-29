using ServiceStack.DataAnnotations;
using System;

namespace Domain.Core
{
    [Alias("CustomerAddress")]
    public class CustomerAddress
    {
       
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public byte Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Remark { get; set; }
        public byte Status { get; set; }
    }
}
