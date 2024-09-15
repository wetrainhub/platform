using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Wth.Crm.EmployeeEmails
{
    public partial interface IEmployeeEmailsAppService : IApplicationService
    {

        Task<PagedResultDto<EmployeeEmailDto>> GetListByEmployeeIdAsync(GetEmployeeEmailListInput input);

        Task<PagedResultDto<EmployeeEmailDto>> GetListAsync(GetEmployeeEmailsInput input);

        Task<EmployeeEmailDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<EmployeeEmailDto> CreateAsync(EmployeeEmailCreateDto input);

        Task<EmployeeEmailDto> UpdateAsync(Guid id, EmployeeEmailUpdateDto input);
    }
}