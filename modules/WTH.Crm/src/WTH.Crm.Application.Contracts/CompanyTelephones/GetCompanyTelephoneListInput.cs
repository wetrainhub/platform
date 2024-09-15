using Volo.Abp.Application.Dtos;
using System;

namespace Wth.Crm.CompanyTelephones
{
    public class GetCompanyTelephoneListInput : PagedAndSortedResultRequestDto
    {
        public Guid CompanyId { get; set; }
    }
}