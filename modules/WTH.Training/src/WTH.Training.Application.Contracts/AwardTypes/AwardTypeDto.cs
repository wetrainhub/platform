using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace WTH.Training.AwardTypes
{
    public abstract class AwardTypeDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; } = null!;
        public bool HasReferenceNumber { get; set; }
        public bool HasExpiryDate { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}