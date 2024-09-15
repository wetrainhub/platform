using Wth.Crm.Shared;
using Asp.Versioning;
using System;
using System.Collections.Generic;
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
    public abstract class CompanyAddressControllerBase : AbpController
    {
        protected ICompanyAddressesAppService _companyAddressesAppService;

        public CompanyAddressControllerBase(ICompanyAddressesAppService companyAddressesAppService)
        {
            _companyAddressesAppService = companyAddressesAppService;
        }

        [HttpGet]
        [Route("by-company")]
        public virtual Task<PagedResultDto<CompanyAddressDto>> GetListByCompanyIdAsync(GetCompanyAddressListInput input)
        {
            return _companyAddressesAppService.GetListByCompanyIdAsync(input);
        }
        [HttpGet]
        [Route("detailed/by-company")]
        public virtual Task<PagedResultDto<CompanyAddressWithNavigationPropertiesDto>> GetListWithNavigationPropertiesByCompanyIdAsync(GetCompanyAddressListInput input)
        {
            return _companyAddressesAppService.GetListWithNavigationPropertiesByCompanyIdAsync(input);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<CompanyAddressWithNavigationPropertiesDto>> GetListAsync(GetCompanyAddressesInput input)
        {
            return _companyAddressesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<CompanyAddressWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _companyAddressesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CompanyAddressDto> GetAsync(Guid id)
        {
            return _companyAddressesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("address-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetAddressLookupAsync(LookupRequestDto input)
        {
            return _companyAddressesAppService.GetAddressLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<CompanyAddressDto> CreateAsync(CompanyAddressCreateDto input)
        {
            return _companyAddressesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CompanyAddressDto> UpdateAsync(Guid id, CompanyAddressUpdateDto input)
        {
            return _companyAddressesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _companyAddressesAppService.DeleteAsync(id);
        }
    }
}