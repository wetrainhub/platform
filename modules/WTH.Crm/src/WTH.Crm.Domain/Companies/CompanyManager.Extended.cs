using Wth.Crm.Notes;
using Wth.Crm.Addresses;
using Wth.Crm.Addresses;
using System;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;

namespace Wth.Crm.Companies
{
    public class CompanyManager : CompanyManagerBase
    {
        //<suite-custom-code-autogenerated>
        public CompanyManager(ICompanyRepository companyRepository,
        IRepository<Note, Guid> noteRepository)
            : base(companyRepository, noteRepository)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}