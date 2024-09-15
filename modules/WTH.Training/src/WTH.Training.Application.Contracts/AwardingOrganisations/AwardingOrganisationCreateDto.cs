using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WTH.Training.AwardingOrganisations
{
    public abstract class AwardingOrganisationCreateDtoBase
    {
        [Required]
        public string Name { get; set; } = null!;
        [EmailAddress]
        public string? Email { get; set; }
        public string? Telephone { get; set; }
    }
}