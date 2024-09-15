using Wth.Crm.Companies;
using Volo.Abp.Application.Dtos;
using System;

namespace Wth.Crm.CompanyAddresses
{
    public abstract class GetCompanyAddressesInputBase : PagedAndSortedResultRequestDto
    {
        public Guid? CompanyId { get; set; }

        public string? FilterText { get; set; }

        public CompanyAddressType? Type { get; set; }
        public Guid? AddressId { get; set; }

        public GetCompanyAddressesInputBase()
        {

        }
    }
}