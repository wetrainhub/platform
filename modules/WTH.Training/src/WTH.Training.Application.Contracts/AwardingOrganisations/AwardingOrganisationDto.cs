using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace WTH.Training.AwardingOrganisations
{
    public abstract class AwardingOrganisationDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Telephone { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}