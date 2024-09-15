using Wth.Crm.Companies;
using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;

namespace Wth.Crm.CompanyEmails
{
    public abstract class CompanyEmailDtoBase : FullAuditedEntityDto<Guid>
    {
        public Guid CompanyId { get; set; }
        public string Value { get; set; } = null!;
        public CompanyEmailType Type { get; set; }

    }
}