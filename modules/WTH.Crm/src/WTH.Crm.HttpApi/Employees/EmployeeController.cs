using Wth.Crm.Shared;
using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Wth.Crm.Employees;

namespace Wth.Crm.Employees
{
    [RemoteService(Name = "Crm")]
    [Area("crm")]
    [ControllerName("Employee")]
    [Route("api/crm/employees")]
    public abstract class EmployeeControllerBase : AbpController
    {
        protected IEmployeesAppService _employeesAppService;

        public EmployeeControllerBase(IEmployeesAppService employeesAppService)
        {
            _employeesAppService = employeesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<EmployeeWithNavigationPropertiesDto>> GetListAsync(GetEmployeesInput input)
        {
            return _employeesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<EmployeeWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _employeesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<EmployeeDto> GetAsync(Guid id)
        {
            return _employeesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("company-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            return _employeesAppService.GetCompanyLookupAsync(input);
        }

        [HttpGet]
        [Route("employee-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeLookupAsync(LookupRequestDto input)
        {
            return _employeesAppService.GetEmployeeLookupAsync(input);
        }

        [HttpGet]
        [Route("note-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetNoteLookupAsync(LookupRequestDto input)
        {
            return _employeesAppService.GetNoteLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<EmployeeDto> CreateAsync(EmployeeCreateDto input)
        {
            return _employeesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<EmployeeDto> UpdateAsync(Guid id, EmployeeUpdateDto input)
        {
            return _employeesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _employeesAppService.DeleteAsync(id);
        }
    }
}