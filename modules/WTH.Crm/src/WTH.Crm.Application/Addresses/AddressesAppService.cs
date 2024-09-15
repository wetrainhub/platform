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
using Wth.Crm.Addresses;

namespace Wth.Crm.Addresses
{

    [Authorize(CrmPermissions.Addresses.Default)]
    public class AddressesAppService : CrmAppService, IAddressesAppService
    {

        protected IAddressRepository _addressRepository;
        protected AddressManager _addressManager;

        public AddressesAppService(IAddressRepository addressRepository, AddressManager addressManager)
        {

            _addressRepository = addressRepository;
            _addressManager = addressManager;

        }

        public virtual async Task<PagedResultDto<AddressDto>> GetListAsync(GetAddressesInput input)
        {
            var totalCount = await _addressRepository.GetCountAsync(input.FilterText, input.Line1, input.Line2, input.Line3, input.City, input.County, input.Postcode);
            var items = await _addressRepository.GetListAsync(input.FilterText, input.Line1, input.Line2, input.Line3, input.City, input.County, input.Postcode, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AddressDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Address>, List<AddressDto>>(items)
            };
        }

        public virtual async Task<AddressDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Address, AddressDto>(await _addressRepository.GetAsync(id));
        }

        [Authorize(CrmPermissions.Addresses.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _addressRepository.DeleteAsync(id);
        }

        [Authorize(CrmPermissions.Addresses.Create)]
        public virtual async Task<AddressDto> CreateAsync(AddressCreateDto input)
        {

            var address = await _addressManager.CreateAsync(
            input.Line1, input.City, input.County, input.Postcode, input.Line2, input.Line3
            );

            return ObjectMapper.Map<Address, AddressDto>(address);
        }

        [Authorize(CrmPermissions.Addresses.Edit)]
        public virtual async Task<AddressDto> UpdateAsync(Guid id, AddressUpdateDto input)
        {

            var address = await _addressManager.UpdateAsync(
            id,
            input.Line1, input.City, input.County, input.Postcode, input.Line2, input.Line3, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Address, AddressDto>(address);
        }
    }
}