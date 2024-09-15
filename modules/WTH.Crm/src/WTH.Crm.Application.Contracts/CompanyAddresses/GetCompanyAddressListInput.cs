using Volo.Abp.Application.Dtos;
using System;

namespace Wth.Crm.CompanyAddresses
{
    public class GetCompanyAddressListInput : PagedAndSortedResultRequestDto
    {
        public Guid CompanyId { get; set; }
    }
}