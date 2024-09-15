using Asp.Versioning;
using System;
using System.Collections.Generic;
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
    public abstract class CompanyTelephoneControllerBase : AbpController
    {
        protected ICompanyTelephonesAppService _companyTelephonesAppService;

        public CompanyTelephoneControllerBase(ICompanyTelephonesAppService companyTelephonesAppService)
        {
            _companyTelephonesAppService = companyTelephonesAppService;
        }

        [HttpGet]
        [Route("by-company")]
        public virtual Task<PagedResultDto<CompanyTelephoneDto>> GetListByCompanyIdAsync(GetCompanyTelephoneListInput input)
        {
            return _companyTelephonesAppService.GetListByCompanyIdAsync(input);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<CompanyTelephoneDto>> GetListAsync(GetCompanyTelephonesInput input)
        {
            return _companyTelephonesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CompanyTelephoneDto> GetAsync(Guid id)
        {
            return _companyTelephonesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<CompanyTelephoneDto> CreateAsync(CompanyTelephoneCreateDto input)
        {
            return _companyTelephonesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CompanyTelephoneDto> UpdateAsync(Guid id, CompanyTelephoneUpdateDto input)
        {
            return _companyTelephonesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _companyTelephonesAppService.DeleteAsync(id);
        }
    }
}