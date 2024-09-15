using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Wth.Crm.CompanyTelephones
{
    public partial interface ICompanyTelephonesAppService : IApplicationService
    {

        Task<PagedResultDto<CompanyTelephoneDto>> GetListByCompanyIdAsync(GetCompanyTelephoneListInput input);

        Task<PagedResultDto<CompanyTelephoneDto>> GetListAsync(GetCompanyTelephonesInput input);

        Task<CompanyTelephoneDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CompanyTelephoneDto> CreateAsync(CompanyTelephoneCreateDto input);

        Task<CompanyTelephoneDto> UpdateAsync(Guid id, CompanyTelephoneUpdateDto input);
    }
}