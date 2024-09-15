using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace Wth.Crm.Addresses
{
    public class AddressManager : DomainService
    {
        protected IAddressRepository _addressRepository;

        public AddressManager(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public virtual async Task<Address> CreateAsync(
        string line1, string city, string county, string postcode, string? line2 = null, string? line3 = null)
        {
            Check.NotNullOrWhiteSpace(line1, nameof(line1));
            Check.NotNullOrWhiteSpace(city, nameof(city));
            Check.NotNullOrWhiteSpace(county, nameof(county));
            Check.NotNullOrWhiteSpace(postcode, nameof(postcode));

            var address = new Address(
             GuidGenerator.Create(),
             line1, city, county, postcode, line2, line3
             );

            return await _addressRepository.InsertAsync(address);
        }

        public virtual async Task<Address> UpdateAsync(
            Guid id,
            string line1, string city, string county, string postcode, string? line2 = null, string? line3 = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(line1, nameof(line1));
            Check.NotNullOrWhiteSpace(city, nameof(city));
            Check.NotNullOrWhiteSpace(county, nameof(county));
            Check.NotNullOrWhiteSpace(postcode, nameof(postcode));

            var address = await _addressRepository.GetAsync(id);

            address.Line1 = line1;
            address.City = city;
            address.County = county;
            address.Postcode = postcode;
            address.Line2 = line2;
            address.Line3 = line3;

            address.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _addressRepository.UpdateAsync(address);
        }

    }
}