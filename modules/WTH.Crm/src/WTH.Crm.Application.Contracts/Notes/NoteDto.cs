using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Wth.Crm.Notes
{
    public abstract class NoteDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Content { get; set; } = null!;

        public string ConcurrencyStamp { get; set; } = null!;

    }
}