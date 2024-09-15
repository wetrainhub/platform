using Wth.Crm.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Wth.Crm.CompanyAddresses
{
    public partial interface ICompanyAddressesAppService : IApplicationService
    {

        Task<PagedResultDto<CompanyAddressDto>> GetListByCompanyIdAsync(GetCompanyAddressListInput input);
        Task<PagedResultDto<CompanyAddressWithNavigationPropertiesDto>> GetListWithNavigationPropertiesByCompanyIdAsync(GetCompanyAddressListInput input);

        Task<PagedResultDto<CompanyAddressWithNavigationPropertiesDto>> GetListAsync(GetCompanyAddressesInput input);

        Task<CompanyAddressWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<CompanyAddressDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetAddressLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CompanyAddressDto> CreateAsync(CompanyAddressCreateDto input);

        Task<CompanyAddressDto> UpdateAsync(Guid id, CompanyAddressUpdateDto input);
    }
}