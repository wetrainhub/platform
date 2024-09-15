using Volo.Abp.Application.Dtos;
using System;

namespace Wth.Crm.CompanyEmails
{
    public class GetCompanyEmailListInput : PagedAndSortedResultRequestDto
    {
        public Guid CompanyId { get; set; }
    }
}