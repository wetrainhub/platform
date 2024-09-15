using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Wth.Crm.CompanyAddresses;

namespace Wth.Crm.CompanyAddresses
{
    [RemoteService(Name = "Crm")]
    [Area("crm")]
    [ControllerName("CompanyAddress")]
    [Route("api/crm/company-addresses")]
    public class CompanyAddressController : CompanyAddressControllerBase, ICompanyAddressesAppService
    {
        public CompanyAddressController(ICompanyAddressesAppService companyAddressesAppService) : base(companyAddressesAppService)
        {
        }
    }
}