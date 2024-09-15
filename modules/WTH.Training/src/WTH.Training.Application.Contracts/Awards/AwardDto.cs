using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace WTH.Training.Awards
{
    public abstract class AwardDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Code { get; set; } = null!;
        public Guid AwardTypeId { get; set; }
        public Guid AwardingOrganisationId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}