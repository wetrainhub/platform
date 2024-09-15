using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Wth.Crm.CompanyEmails
{
    public partial interface ICompanyEmailsAppService : IApplicationService
    {

        Task<PagedResultDto<CompanyEmailDto>> GetListByCompanyIdAsync(GetCompanyEmailListInput input);

        Task<PagedResultDto<CompanyEmailDto>> GetListAsync(GetCompanyEmailsInput input);

        Task<CompanyEmailDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CompanyEmailDto> CreateAsync(CompanyEmailCreateDto input);

        Task<CompanyEmailDto> UpdateAsync(Guid id, CompanyEmailUpdateDto input);
    }
}