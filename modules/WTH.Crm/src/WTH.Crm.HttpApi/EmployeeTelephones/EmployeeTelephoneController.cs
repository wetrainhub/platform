using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Wth.Crm.EmployeeTelephones;

namespace Wth.Crm.EmployeeTelephones
{
    [RemoteService(Name = "Crm")]
    [Area("crm")]
    [ControllerName("EmployeeTelephone")]
    [Route("api/crm/employee-telephones")]
    public abstract class EmployeeTelephoneControllerBase : AbpController
    {
        protected IEmployeeTelephonesAppService _employeeTelephonesAppService;

        public EmployeeTelephoneControllerBase(IEmployeeTelephonesAppService employeeTelephonesAppService)
        {
            _employeeTelephonesAppService = employeeTelephonesAppService;
        }

        [HttpGet]
        [Route("by-employee")]
        public virtual Task<PagedResultDto<EmployeeTelephoneDto>> GetListByEmployeeIdAsync(GetEmployeeTelephoneListInput input)
        {
            return _employeeTelephonesAppService.GetListByEmployeeIdAsync(input);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<EmployeeTelephoneDto>> GetListAsync(GetEmployeeTelephonesInput input)
        {
            return _employeeTelephonesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<EmployeeTelephoneDto> GetAsync(Guid id)
        {
            return _employeeTelephonesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<EmployeeTelephoneDto> CreateAsync(EmployeeTelephoneCreateDto input)
        {
            return _employeeTelephonesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<EmployeeTelephoneDto> UpdateAsync(Guid id, EmployeeTelephoneUpdateDto input)
        {
            return _employeeTelephonesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _employeeTelephonesAppService.DeleteAsync(id);
        }
    }
}