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
using Wth.Crm.EmployeeTelephones;

namespace Wth.Crm.EmployeeTelephones
{

    [Authorize(CrmPermissions.EmployeeTelephones.Default)]
    public abstract class EmployeeTelephonesAppServiceBase : CrmAppService
    {

        protected IEmployeeTelephoneRepository _employeeTelephoneRepository;
        protected EmployeeTelephoneManager _employeeTelephoneManager;

        public EmployeeTelephonesAppServiceBase(IEmployeeTelephoneRepository employeeTelephoneRepository, EmployeeTelephoneManager employeeTelephoneManager)
        {

            _employeeTelephoneRepository = employeeTelephoneRepository;
            _employeeTelephoneManager = employeeTelephoneManager;

        }

        public virtual async Task<PagedResultDto<EmployeeTelephoneDto>> GetListByEmployeeIdAsync(GetEmployeeTelephoneListInput input)
        {
            var employeeTelephones = await _employeeTelephoneRepository.GetListByEmployeeIdAsync(
                input.EmployeeId,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount);

            return new PagedResultDto<EmployeeTelephoneDto>
            {
                TotalCount = await _employeeTelephoneRepository.GetCountByEmployeeIdAsync(input.EmployeeId),
                Items = ObjectMapper.Map<List<EmployeeTelephone>, List<EmployeeTelephoneDto>>(employeeTelephones)
            };
        }

        public virtual async Task<PagedResultDto<EmployeeTelephoneDto>> GetListAsync(GetEmployeeTelephonesInput input)
        {
            var totalCount = await _employeeTelephoneRepository.GetCountAsync(input.FilterText, input.Value, input.Type);
            var items = await _employeeTelephoneRepository.GetListAsync(input.FilterText, input.Value, input.Type, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<EmployeeTelephoneDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<EmployeeTelephone>, List<EmployeeTelephoneDto>>(items)
            };
        }

        public virtual async Task<EmployeeTelephoneDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<EmployeeTelephone, EmployeeTelephoneDto>(await _employeeTelephoneRepository.GetAsync(id));
        }

        [Authorize(CrmPermissions.EmployeeTelephones.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _employeeTelephoneRepository.DeleteAsync(id);
        }

        [Authorize(CrmPermissions.EmployeeTelephones.Create)]
        public virtual async Task<EmployeeTelephoneDto> CreateAsync(EmployeeTelephoneCreateDto input)
        {

            var employeeTelephone = await _employeeTelephoneManager.CreateAsync(input.EmployeeId
            , input.Value, input.Type
            );

            return ObjectMapper.Map<EmployeeTelephone, EmployeeTelephoneDto>(employeeTelephone);
        }

        [Authorize(CrmPermissions.EmployeeTelephones.Edit)]
        public virtual async Task<EmployeeTelephoneDto> UpdateAsync(Guid id, EmployeeTelephoneUpdateDto input)
        {

            var employeeTelephone = await _employeeTelephoneManager.UpdateAsync(
            id, input.EmployeeId
            , input.Value, input.Type
            );

            return ObjectMapper.Map<EmployeeTelephone, EmployeeTelephoneDto>(employeeTelephone);
        }
    }
}