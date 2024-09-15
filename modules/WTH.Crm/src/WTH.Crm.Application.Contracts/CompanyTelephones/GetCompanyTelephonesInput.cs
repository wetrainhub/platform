using Wth.Crm.Companies;
using Volo.Abp.Application.Dtos;
using System;

namespace Wth.Crm.CompanyTelephones
{
    public abstract class GetCompanyTelephonesInputBase : PagedAndSortedResultRequestDto
    {
        public Guid? CompanyId { get; set; }

        public string? FilterText { get; set; }

        public string? Value { get; set; }
        public CompanyTelephoneType? Type { get; set; }

        public GetCompanyTelephonesInputBase()
        {

        }
    }
}