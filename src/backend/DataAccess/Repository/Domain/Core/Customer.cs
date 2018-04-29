using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
namespace Domain.Core
{
    [Alias("Customers")]
    public class Customer
    {
        public Customer()
        {
            this.Addresses = new List<CustomerAddress>();
        }
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string PanNo { get; set; }
        public string Website { get; set; }
        public string Bio { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        [Reference]
        public List<CustomerAddress> Addresses { get; set; }

    }
}
