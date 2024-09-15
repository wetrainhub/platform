using Wth.Crm.Shared;
using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Wth.Crm.EmployeeAddresses;

namespace Wth.Crm.EmployeeAddresses
{
    [RemoteService(Name = "Crm")]
    [Area("crm")]
    [ControllerName("EmployeeAddress")]
    [Route("api/crm/employee-addresses")]
    public abstract class EmployeeAddressControllerBase : AbpController
    {
        protected IEmployeeAddressesAppService _employeeAddressesAppService;

        public EmployeeAddressControllerBase(IEmployeeAddressesAppService employeeAddressesAppService)
        {
            _employeeAddressesAppService = employeeAddressesAppService;
        }

        [HttpGet]
        [Route("by-employee")]
        public virtual Task<PagedResultDto<EmployeeAddressDto>> GetListByEmployeeIdAsync(GetEmployeeAddressListInput input)
        {
            return _employeeAddressesAppService.GetListByEmployeeIdAsync(input);
        }
        [HttpGet]
        [Route("detailed/by-employee")]
        public virtual Task<PagedResultDto<EmployeeAddressWithNavigationPropertiesDto>> GetListWithNavigationPropertiesByEmployeeIdAsync(GetEmployeeAddressListInput input)
        {
            return _employeeAddressesAppService.GetListWithNavigationPropertiesByEmployeeIdAsync(input);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<EmployeeAddressWithNavigationPropertiesDto>> GetListAsync(GetEmployeeAddressesInput input)
        {
            return _employeeAddressesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<EmployeeAddressWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _employeeAddressesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<EmployeeAddressDto> GetAsync(Guid id)
        {
            return _employeeAddressesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("address-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetAddressLookupAsync(LookupRequestDto input)
        {
            return _employeeAddressesAppService.GetAddressLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<EmployeeAddressDto> CreateAsync(EmployeeAddressCreateDto input)
        {
            return _employeeAddressesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<EmployeeAddressDto> UpdateAsync(Guid id, EmployeeAddressUpdateDto input)
        {
            return _employeeAddressesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _employeeAddressesAppService.DeleteAsync(id);
        }
    }
}