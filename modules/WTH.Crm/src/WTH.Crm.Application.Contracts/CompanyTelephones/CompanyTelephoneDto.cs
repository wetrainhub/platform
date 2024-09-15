using Wth.Crm.Companies;
using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;

namespace Wth.Crm.CompanyTelephones
{
    public abstract class CompanyTelephoneDtoBase : FullAuditedEntityDto<Guid>
    {
        public Guid CompanyId { get; set; }
        public string Value { get; set; } = null!;
        public CompanyTelephoneType Type { get; set; }

    }
}