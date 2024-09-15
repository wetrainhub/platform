using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Wth.Crm.CompanyTelephones;

namespace Wth.Crm.CompanyTelephones
{
    [RemoteService(Name = "Crm")]
    [Area("crm")]
    [ControllerName("CompanyTelephone")]
    [Route("api/crm/company-telephones")]
    public class CompanyTelephoneController : CompanyTelephoneControllerBase, ICompanyTelephonesAppService
    {
        public CompanyTelephoneController(ICompanyTelephonesAppService companyTelephonesAppService) : base(companyTelephonesAppService)
        {
        }
    }
}