using Wth.Crm.Companies;
using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;

namespace Wth.Crm.CompanyAddresses
{
    public abstract class CompanyAddressDtoBase : FullAuditedEntityDto<Guid>
    {
        public Guid CompanyId { get; set; }
        public CompanyAddressType Type { get; set; }
        public Guid AddressId { get; set; }

    }
}