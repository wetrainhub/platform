using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Wth.Crm.Addresses
{
    public class AddressCreateDto
    {
        [Required]
        public string Line1 { get; set; } = null!;
        public string? Line2 { get; set; }
        public string? Line3 { get; set; }
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string County { get; set; } = null!;
        [Required]
        public string Postcode { get; set; } = null!;
    }
}