using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Wth.Crm.Permissions;
using Wth.Crm.EmployeeEmails;

namespace Wth.Crm.EmployeeEmails
{

    [Authorize(CrmPermissions.EmployeeEmails.Default)]
    public abstract class EmployeeEmailsAppServiceBase : CrmAppService
    {

        protected IEmployeeEmailRepository _employeeEmailRepository;
        protected EmployeeEmailManager _employeeEmailManager;

        public EmployeeEmailsAppServiceBase(IEmployeeEmailRepository employeeEmailRepository, EmployeeEmailManager employeeEmailManager)
        {

            _employeeEmailRepository = employeeEmailRepository;
            _employeeEmailManager = employeeEmailManager;

        }

        public virtual async Task<PagedResultDto<EmployeeEmailDto>> GetListByEmployeeIdAsync(GetEmployeeEmailListInput input)
        {
            var employeeEmails = await _employeeEmailRepository.GetListByEmployeeIdAsync(
                input.EmployeeId,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount);

            return new PagedResultDto<EmployeeEmailDto>
            {
                TotalCount = await _employeeEmailRepository.GetCountByEmployeeIdAsync(input.EmployeeId),
                Items = ObjectMapper.Map<List<EmployeeEmail>, List<EmployeeEmailDto>>(employeeEmails)
            };
        }

        public virtual async Task<PagedResultDto<EmployeeEmailDto>> GetListAsync(GetEmployeeEmailsInput input)
        {
            var totalCount = await _employeeEmailRepository.GetCountAsync(input.FilterText, input.Value, input.Type);
            var items = await _employeeEmailRepository.GetListAsync(input.FilterText, input.Value, input.Type, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<EmployeeEmailDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<EmployeeEmail>, List<EmployeeEmailDto>>(items)
            };
        }

        public virtual async Task<EmployeeEmailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<EmployeeEmail, EmployeeEmailDto>(await _employeeEmailRepository.GetAsync(id));
        }

        [Authorize(CrmPermissions.EmployeeEmails.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _employeeEmailRepository.DeleteAsync(id);
        }

        [Authorize(CrmPermissions.EmployeeEmails.Create)]
        public virtual async Task<EmployeeEmailDto> CreateAsync(EmployeeEmailCreateDto input)
        {

            var employeeEmail = await _employeeEmailManager.CreateAsync(input.EmployeeId
            , input.Value, input.Type
            );

            return ObjectMapper.Map<EmployeeEmail, EmployeeEmailDto>(employeeEmail);
        }

        [Authorize(CrmPermissions.EmployeeEmails.Edit)]
        public virtual async Task<EmployeeEmailDto> UpdateAsync(Guid id, EmployeeEmailUpdateDto input)
        {

            var employeeEmail = await _employeeEmailManager.UpdateAsync(
            id, input.EmployeeId
            , input.Value, input.Type
            );

            return ObjectMapper.Map<EmployeeEmail, EmployeeEmailDto>(employeeEmail);
        }
    }
}