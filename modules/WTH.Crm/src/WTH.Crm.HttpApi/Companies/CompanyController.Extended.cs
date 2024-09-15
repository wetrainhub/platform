using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Wth.Crm.Companies;

namespace Wth.Crm.Companies
{
    [RemoteService(Name = "Crm")]
    [Area("crm")]
    [ControllerName("Company")]
    [Route("api/crm/companies")]
    public class CompanyController : CompanyControllerBase, ICompaniesAppService
    {
        public CompanyController(ICompaniesAppService companiesAppService) : base(companiesAppService)
        {
        }
    }
}