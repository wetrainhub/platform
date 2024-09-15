using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Wth.Crm.EmployeeEmails;

namespace Wth.Crm.EmployeeEmails
{
    [RemoteService(Name = "Crm")]
    [Area("crm")]
    [ControllerName("EmployeeEmail")]
    [Route("api/crm/employee-emails")]
    public abstract class EmployeeEmailControllerBase : AbpController
    {
        protected IEmployeeEmailsAppService _employeeEmailsAppService;

        public EmployeeEmailControllerBase(IEmployeeEmailsAppService employeeEmailsAppService)
        {
            _employeeEmailsAppService = employeeEmailsAppService;
        }

        [HttpGet]
        [Route("by-employee")]
        public virtual Task<PagedResultDto<EmployeeEmailDto>> GetListByEmployeeIdAsync(GetEmployeeEmailListInput input)
        {
            return _employeeEmailsAppService.GetListByEmployeeIdAsync(input);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<EmployeeEmailDto>> GetListAsync(GetEmployeeEmailsInput input)
        {
            return _employeeEmailsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<EmployeeEmailDto> GetAsync(Guid id)
        {
            return _employeeEmailsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<EmployeeEmailDto> CreateAsync(EmployeeEmailCreateDto input)
        {
            return _employeeEmailsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<EmployeeEmailDto> UpdateAsync(Guid id, EmployeeEmailUpdateDto input)
        {
            return _employeeEmailsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _employeeEmailsAppService.DeleteAsync(id);
        }
    }
}