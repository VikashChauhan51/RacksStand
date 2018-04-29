using System;
using System.ComponentModel.DataAnnotations;


namespace Models.Core
{
    [Serializable]
    public class AddressModel
    {
        [Required]
        [MaxLength(32)]
        public string Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [Range(0, 255)]
        public byte Country { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Fax { get; set; }
        [Phone]
        public string Phone { get; set; }
        public string Remark { get; set; }
        [Required]
        [MaxLength(50)]
        public string State { get; set; }
        public byte Status { get; set; }
        [Required]
        [MaxLength(150)]
        public string Street { get; set; }
        [Required]
        [MaxLength(20)]
        public string ZipCode { get; set; }
    }
}
