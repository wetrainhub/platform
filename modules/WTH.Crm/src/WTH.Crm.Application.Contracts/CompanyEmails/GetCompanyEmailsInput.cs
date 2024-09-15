using Wth.Crm.Companies;
using Volo.Abp.Application.Dtos;
using System;

namespace Wth.Crm.CompanyEmails
{
    public abstract class GetCompanyEmailsInputBase : PagedAndSortedResultRequestDto
    {
        public Guid? CompanyId { get; set; }

        public string? FilterText { get; set; }

        public string? Value { get; set; }
        public CompanyEmailType? Type { get; set; }

        public GetCompanyEmailsInputBase()
        {

        }
    }
}