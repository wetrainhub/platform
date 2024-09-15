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
using Wth.Crm.CompanyAddresses;

namespace Wth.Crm.CompanyAddresses
{

    [Authorize(CrmPermissions.CompanyAddresses.Default)]
    public abstract class CompanyAddressesAppServiceBase : CrmAppService
    {

        protected ICompanyAddressRepository _companyAddressRepository;
        protected CompanyAddressManager _companyAddressManager;

        protected IRepository<Wth.Crm.Addresses.Address, Guid> _addressRepository;

        public CompanyAddressesAppServiceBase(ICompanyAddressRepository companyAddressRepository, CompanyAddressManager companyAddressManager, IRepository<Wth.Crm.Addresses.Address, Guid> addressRepository)
        {

            _companyAddressRepository = companyAddressRepository;
            _companyAddressManager = companyAddressManager; _addressRepository = addressRepository;

        }

        public virtual async Task<PagedResultDto<CompanyAddressDto>> GetListByCompanyIdAsync(GetCompanyAddressListInput input)
        {
            var companyAddresses = await _companyAddressRepository.GetListByCompanyIdAsync(
                input.CompanyId,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount);

            return new PagedResultDto<CompanyAddressDto>
            {
                TotalCount = await _companyAddressRepository.GetCountByCompanyIdAsync(input.CompanyId),
                Items = ObjectMapper.Map<List<CompanyAddress>, List<CompanyAddressDto>>(companyAddresses)
            };
        }
        public virtual async Task<PagedResultDto<CompanyAddressWithNavigationPropertiesDto>> GetListWithNavigationPropertiesByCompanyIdAsync(GetCompanyAddressListInput input)
        {
            var companyAddresses = await _companyAddressRepository.GetListWithNavigationPropertiesByCompanyIdAsync(
                input.CompanyId,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount);

            return new PagedResultDto<CompanyAddressWithNavigationPropertiesDto>
            {
                TotalCount = await _companyAddressRepository.GetCountByCompanyIdAsync(input.CompanyId),
                Items = ObjectMapper.Map<List<CompanyAddressWithNavigationProperties>, List<CompanyAddressWithNavigationPropertiesDto>>(companyAddresses)
            };
        }

        public virtual async Task<PagedResultDto<CompanyAddressWithNavigationPropertiesDto>> GetListAsync(GetCompanyAddressesInput input)
        {
            var totalCount = await _companyAddressRepository.GetCountAsync(input.FilterText, input.Type, input.AddressId);
            var items = await _companyAddressRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Type, input.AddressId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CompanyAddressWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CompanyAddressWithNavigationProperties>, List<CompanyAddressWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CompanyAddressWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CompanyAddressWithNavigationProperties, CompanyAddressWithNavigationPropertiesDto>
                (await _companyAddressRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<CompanyAddressDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CompanyAddress, CompanyAddressDto>(await _companyAddressRepository.GetAsync(id));
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

        [Authorize(CrmPermissions.CompanyAddresses.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _companyAddressRepository.DeleteAsync(id);
        }

        [Authorize(CrmPermissions.CompanyAddresses.Create)]
        public virtual async Task<CompanyAddressDto> CreateAsync(CompanyAddressCreateDto input)
        {
            if (input.AddressId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Address"]]);
            }

            var companyAddress = await _companyAddressManager.CreateAsync(input.CompanyId
            , input.AddressId, input.Type
            );

            return ObjectMapper.Map<CompanyAddress, CompanyAddressDto>(companyAddress);
        }

        [Authorize(CrmPermissions.CompanyAddresses.Edit)]
        public virtual async Task<CompanyAddressDto> UpdateAsync(Guid id, CompanyAddressUpdateDto input)
        {
            if (input.AddressId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Address"]]);
            }

            var companyAddress = await _companyAddressManager.UpdateAsync(
            id, input.CompanyId
            , input.AddressId, input.Type
            );

            return ObjectMapper.Map<CompanyAddress, CompanyAddressDto>(companyAddress);
        }
    }
}