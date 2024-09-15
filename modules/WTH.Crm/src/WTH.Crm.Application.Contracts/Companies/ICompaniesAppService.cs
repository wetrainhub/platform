using Wth.Crm.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Wth.Crm.Companies
{
    public partial interface ICompaniesAppService : IApplicationService
    {

        Task<PagedResultDto<CompanyWithNavigationPropertiesDto>> GetListAsync(GetCompaniesInput input);

        Task<CompanyWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<CompanyDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetNoteLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CompanyDto> CreateAsync(CompanyCreateDto input);

        Task<CompanyDto> UpdateAsync(Guid id, CompanyUpdateDto input);
    }
}