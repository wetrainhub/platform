using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace WTH.Training.AwardingOrganisations
{
    public abstract class AwardingOrganisationUpdateDtoBase : IHasConcurrencyStamp
    {
        [Required]
        public string Name { get; set; } = null!;
        [EmailAddress]
        public string? Email { get; set; }
        public string? Telephone { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}