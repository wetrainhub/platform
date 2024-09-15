using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Wth.Crm.Addresses;

namespace Wth.Crm.Addresses
{
    [RemoteService(Name = "Crm")]
    [Area("crm")]
    [ControllerName("Address")]
    [Route("api/crm/addresses")]
    public class AddressController : AbpController, IAddressesAppService
    {
        protected IAddressesAppService _addressesAppService;

        public AddressController(IAddressesAppService addressesAppService)
        {
            _addressesAppService = addressesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<AddressDto>> GetListAsync(GetAddressesInput input)
        {
            return _addressesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<AddressDto> GetAsync(Guid id)
        {
            return _addressesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<AddressDto> CreateAsync(AddressCreateDto input)
        {
            return _addressesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<AddressDto> UpdateAsync(Guid id, AddressUpdateDto input)
        {
            return _addressesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _addressesAppService.DeleteAsync(id);
        }
    }
}