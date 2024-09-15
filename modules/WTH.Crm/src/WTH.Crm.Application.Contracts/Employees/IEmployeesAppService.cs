using Wth.Crm.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Wth.Crm.Employees
{
    public partial interface IEmployeesAppService : IApplicationService
    {

        Task<PagedResultDto<EmployeeWithNavigationPropertiesDto>> GetListAsync(GetEmployeesInput input);

        Task<EmployeeWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<EmployeeDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetNoteLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<EmployeeDto> CreateAsync(EmployeeCreateDto input);

        Task<EmployeeDto> UpdateAsync(Guid id, EmployeeUpdateDto input);
    }
}