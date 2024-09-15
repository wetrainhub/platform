using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WTH.Training.Awards
{
    public abstract class AwardCreateDtoBase
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        public string Code { get; set; } = null!;
        public Guid AwardTypeId { get; set; }
        public Guid AwardingOrganisationId { get; set; }
    }
}