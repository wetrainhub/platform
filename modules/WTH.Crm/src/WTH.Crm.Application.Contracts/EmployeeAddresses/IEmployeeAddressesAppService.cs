using Wth.Crm.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Wth.Crm.EmployeeAddresses
{
    public partial interface IEmployeeAddressesAppService : IApplicationService
    {

        Task<PagedResultDto<EmployeeAddressDto>> GetListByEmployeeIdAsync(GetEmployeeAddressListInput input);
        Task<PagedResultDto<EmployeeAddressWithNavigationPropertiesDto>> GetListWithNavigationPropertiesByEmployeeIdAsync(GetEmployeeAddressListInput input);

        Task<PagedResultDto<EmployeeAddressWithNavigationPropertiesDto>> GetListAsync(GetEmployeeAddressesInput input);

        Task<EmployeeAddressWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<EmployeeAddressDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetAddressLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<EmployeeAddressDto> CreateAsync(EmployeeAddressCreateDto input);

        Task<EmployeeAddressDto> UpdateAsync(Guid id, EmployeeAddressUpdateDto input);
    }
}