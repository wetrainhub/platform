using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Wth.Crm.CompanyEmails;

namespace Wth.Crm.CompanyEmails
{
    [RemoteService(Name = "Crm")]
    [Area("crm")]
    [ControllerName("CompanyEmail")]
    [Route("api/crm/company-emails")]
    public class CompanyEmailController : CompanyEmailControllerBase, ICompanyEmailsAppService
    {
        public CompanyEmailController(ICompanyEmailsAppService companyEmailsAppService) : base(companyEmailsAppService)
        {
        }
    }
}