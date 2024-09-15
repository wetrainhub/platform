using Wth.Crm.Shared;
using Wth.Crm.Notes;
using Wth.Crm.Employees;
using Wth.Crm.Companies;
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
using Wth.Crm.Employees;

namespace Wth.Crm.Employees
{

    [Authorize(CrmPermissions.Employees.Default)]
    public abstract class EmployeesAppServiceBase : CrmAppService
    {

        protected IEmployeeRepository _employeeRepository;
        protected EmployeeManager _employeeManager;

        protected IRepository<Wth.Crm.Companies.Company, Guid> _companyRepository;
        protected IRepository<Wth.Crm.Notes.Note, Guid> _noteRepository;

        public EmployeesAppServiceBase(IEmployeeRepository employeeRepository, EmployeeManager employeeManager, IRepository<Wth.Crm.Companies.Company, Guid> companyRepository, IRepository<Wth.Crm.Notes.Note, Guid> noteRepository)
        {

            _employeeRepository = employeeRepository;
            _employeeManager = employeeManager; _companyRepository = companyRepository;
            _noteRepository = noteRepository;

        }

        public virtual async Task<PagedResultDto<EmployeeWithNavigationPropertiesDto>> GetListAsync(GetEmployeesInput input)
        {
            var totalCount = await _employeeRepository.GetCountAsync(input.FilterText, input.FirstName, input.LastName, input.IdentityNumber, input.EnrolmentNumber, input.Status, input.Type, input.CompanyId, input.EmployeeId, input.NoteId);
            var items = await _employeeRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.FirstName, input.LastName, input.IdentityNumber, input.EnrolmentNumber, input.Status, input.Type, input.CompanyId, input.EmployeeId, input.NoteId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<EmployeeWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<EmployeeWithNavigationProperties>, List<EmployeeWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<EmployeeWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<EmployeeWithNavigationProperties, EmployeeWithNavigationPropertiesDto>
                (await _employeeRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<EmployeeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Employee, EmployeeDto>(await _employeeRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            var query = (await _companyRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Wth.Crm.Companies.Company>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Wth.Crm.Companies.Company>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeLookupAsync(LookupRequestDto input)
        {
            var query = (await _employeeRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.FirstName != null &&
                         x.FirstName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Wth.Crm.Employees.Employee>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Wth.Crm.Employees.Employee>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetNoteLookupAsync(LookupRequestDto input)
        {
            var query = (await _noteRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Content != null &&
                         x.Content.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Wth.Crm.Notes.Note>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Wth.Crm.Notes.Note>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(CrmPermissions.Employees.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _employeeRepository.DeleteAsync(id);
        }

        [Authorize(CrmPermissions.Employees.Create)]
        public virtual async Task<EmployeeDto> CreateAsync(EmployeeCreateDto input)
        {

            var employee = await _employeeManager.CreateAsync(
            input.NoteIds, input.CompanyId, input.EmployeeId, input.FirstName, input.LastName, input.Status, input.Type, input.MiddleName, input.IdentityNumber, input.EnrolmentNumber, input.DateOfBirth
            );

            return ObjectMapper.Map<Employee, EmployeeDto>(employee);
        }

        [Authorize(CrmPermissions.Employees.Edit)]
        public virtual async Task<EmployeeDto> UpdateAsync(Guid id, EmployeeUpdateDto input)
        {

            var employee = await _employeeManager.UpdateAsync(
            id,
            input.NoteIds, input.CompanyId, input.EmployeeId, input.FirstName, input.LastName, input.Status, input.Type, input.MiddleName, input.IdentityNumber, input.DateOfBirth, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Employee, EmployeeDto>(employee);
        }
    }
}