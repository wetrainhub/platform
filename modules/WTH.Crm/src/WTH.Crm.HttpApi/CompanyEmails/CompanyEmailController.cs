using Asp.Versioning;
using System;
using System.Collections.Generic;
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
    public abstract class CompanyEmailControllerBase : AbpController
    {
        protected ICompanyEmailsAppService _companyEmailsAppService;

        public CompanyEmailControllerBase(ICompanyEmailsAppService companyEmailsAppService)
        {
            _companyEmailsAppService = companyEmailsAppService;
        }

        [HttpGet]
        [Route("by-company")]
        public virtual Task<PagedResultDto<CompanyEmailDto>> GetListByCompanyIdAsync(GetCompanyEmailListInput input)
        {
            return _companyEmailsAppService.GetListByCompanyIdAsync(input);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<CompanyEmailDto>> GetListAsync(GetCompanyEmailsInput input)
        {
            return _companyEmailsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CompanyEmailDto> GetAsync(Guid id)
        {
            return _companyEmailsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<CompanyEmailDto> CreateAsync(CompanyEmailCreateDto input)
        {
            return _companyEmailsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CompanyEmailDto> UpdateAsync(Guid id, CompanyEmailUpdateDto input)
        {
            return _companyEmailsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _companyEmailsAppService.DeleteAsync(id);
        }
    }
}