using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Wth.Crm.EmployeeTelephones
{
    public partial interface IEmployeeTelephonesAppService : IApplicationService
    {

        Task<PagedResultDto<EmployeeTelephoneDto>> GetListByEmployeeIdAsync(GetEmployeeTelephoneListInput input);

        Task<PagedResultDto<EmployeeTelephoneDto>> GetListAsync(GetEmployeeTelephonesInput input);

        Task<EmployeeTelephoneDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<EmployeeTelephoneDto> CreateAsync(EmployeeTelephoneCreateDto input);

        Task<EmployeeTelephoneDto> UpdateAsync(Guid id, EmployeeTelephoneUpdateDto input);
    }
}