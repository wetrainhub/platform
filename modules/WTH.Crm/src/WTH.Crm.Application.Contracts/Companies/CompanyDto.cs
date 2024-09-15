using System;
using System.Collections.Generic;
using Wth.Crm.CompanyAddresses;
using Wth.Crm.CompanyEmails;
using Wth.Crm.CompanyTelephones;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Wth.Crm.Companies
{
    public abstract class CompanyDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; } = null!;
        public string? TaxReference { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

        public List<CompanyAddressWithNavigationPropertiesDto> CompanyAddresses { get; set; } = new();
        public List<CompanyEmailDto> CompanyEmails { get; set; } = new();
        public List<CompanyTelephoneDto> CompanyTelephones { get; set; } = new();
    }
}