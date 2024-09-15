using Wth.Crm.Shared;
using Asp.Versioning;
using System;
using System.Collections.Generic;
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
    public abstract class CompanyControllerBase : AbpController
    {
        protected ICompaniesAppService _companiesAppService;

        public CompanyControllerBase(ICompaniesAppService companiesAppService)
        {
            _companiesAppService = companiesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<CompanyWithNavigationPropertiesDto>> GetListAsync(GetCompaniesInput input)
        {
            return _companiesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<CompanyWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _companiesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CompanyDto> GetAsync(Guid id)
        {
            return _companiesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("note-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetNoteLookupAsync(LookupRequestDto input)
        {
            return _companiesAppService.GetNoteLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<CompanyDto> CreateAsync(CompanyCreateDto input)
        {
            return _companiesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CompanyDto> UpdateAsync(Guid id, CompanyUpdateDto input)
        {
            return _companiesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _companiesAppService.DeleteAsync(id);
        }
    }
}