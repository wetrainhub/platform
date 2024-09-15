using Wth.Crm.Shared;
using Wth.Crm.Addresses;
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
using Wth.Crm.EmployeeAddresses;

namespace Wth.Crm.EmployeeAddresses
{

    [Authorize(CrmPermissions.EmployeeAddresses.Default)]
    public abstract class EmployeeAddressesAppServiceBase : CrmAppService
    {

        protected IEmployeeAddressRepository _employeeAddressRepository;
        protected EmployeeAddressManager _employeeAddressManager;

        protected IRepository<Wth.Crm.Addresses.Address, Guid> _addressRepository;

        public EmployeeAddressesAppServiceBase(IEmployeeAddressRepository employeeAddressRepository, EmployeeAddressManager employeeAddressManager, IRepository<Wth.Crm.Addresses.Address, Guid> addressRepository)
        {

            _employeeAddressRepository = employeeAddressRepository;
            _employeeAddressManager = employeeAddressManager; _addressRepository = addressRepository;

        }

        public virtual async Task<PagedResultDto<EmployeeAddressDto>> GetListByEmployeeIdAsync(GetEmployeeAddressListInput input)
        {
            var employeeAddresses = await _employeeAddressRepository.GetListByEmployeeIdAsync(
                input.EmployeeId,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount);

            return new PagedResultDto<EmployeeAddressDto>
            {
                TotalCount = await _employeeAddressRepository.GetCountByEmployeeIdAsync(input.EmployeeId),
                Items = ObjectMapper.Map<List<EmployeeAddress>, List<EmployeeAddressDto>>(employeeAddresses)
            };
        }
        public virtual async Task<PagedResultDto<EmployeeAddressWithNavigationPropertiesDto>> GetListWithNavigationPropertiesByEmployeeIdAsync(GetEmployeeAddressListInput input)
        {
            var employeeAddresses = await _employeeAddressRepository.GetListWithNavigationPropertiesByEmployeeIdAsync(
                input.EmployeeId,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount);

            return new PagedResultDto<EmployeeAddressWithNavigationPropertiesDto>
            {
                TotalCount = await _employeeAddressRepository.GetCountByEmployeeIdAsync(input.EmployeeId),
                Items = ObjectMapper.Map<List<EmployeeAddressWithNavigationProperties>, List<EmployeeAddressWithNavigationPropertiesDto>>(employeeAddresses)
            };
        }

        public virtual async Task<PagedResultDto<EmployeeAddressWithNavigationPropertiesDto>> GetListAsync(GetEmployeeAddressesInput input)
        {
            var totalCount = await _employeeAddressRepository.GetCountAsync(input.FilterText, input.Type, input.AddressId);
            var items = await _employeeAddressRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Type, input.AddressId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<EmployeeAddressWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<EmployeeAddressWithNavigationProperties>, List<EmployeeAddressWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<EmployeeAddressWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<EmployeeAddressWithNavigationProperties, EmployeeAddressWithNavigationPropertiesDto>
                (await _employeeAddressRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<EmployeeAddressDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<EmployeeAddress, EmployeeAddressDto>(await _employeeAddressRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetAddressLookupAsync(LookupRequestDto input)
        {
            var query = (await _addressRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Line1 != null &&
                         x.Line1.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Wth.Crm.Addresses.Address>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Wth.Crm.Addresses.Address>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(CrmPermissions.EmployeeAddresses.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _employeeAddressRepository.DeleteAsync(id);
        }

        [Authorize(CrmPermissions.EmployeeAddresses.Create)]
        public virtual async Task<EmployeeAddressDto> CreateAsync(EmployeeAddressCreateDto input)
        {
            if (input.AddressId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Address"]]);
            }

            var employeeAddress = await _employeeAddressManager.CreateAsync(input.EmployeeId
            , input.AddressId, input.Type
            );

            return ObjectMapper.Map<EmployeeAddress, EmployeeAddressDto>(employeeAddress);
        }

        [Authorize(CrmPermissions.EmployeeAddresses.Edit)]
        public virtual async Task<EmployeeAddressDto> UpdateAsync(Guid id, EmployeeAddressUpdateDto input)
        {
            if (input.AddressId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Address"]]);
            }

            var employeeAddress = await _employeeAddressManager.UpdateAsync(
            id, input.EmployeeId
            , input.AddressId, input.Type
            );

            return ObjectMapper.Map<EmployeeAddress, EmployeeAddressDto>(employeeAddress);
        }
    }
}