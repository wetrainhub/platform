using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace WTH.Training.Awards
{
    public abstract class AwardUpdateDtoBase : IHasConcurrencyStamp
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        public string Code { get; set; } = null!;
        public Guid AwardTypeId { get; set; }
        public Guid AwardingOrganisationId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}