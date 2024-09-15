using Wth.Crm.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Wth.Crm.CompanyAddresses
{
    public abstract class CompanyAddressManagerBase : DomainService
    {
        protected ICompanyAddressRepository _companyAddressRepository;

        public CompanyAddressManagerBase(ICompanyAddressRepository companyAddressRepository)
        {
            _companyAddressRepository = companyAddressRepository;
        }

        public virtual async Task<CompanyAddress> CreateAsync(
        Guid companyId, Guid addressId, CompanyAddressType type)
        {
            Check.NotNull(addressId, nameof(addressId));
            Check.NotNull(type, nameof(type));

            var companyAddress = new CompanyAddress(
             GuidGenerator.Create(),
             companyId, addressId, type
             );

            return await _companyAddressRepository.InsertAsync(companyAddress);
        }

        public virtual async Task<CompanyAddress> UpdateAsync(
            Guid id,
            Guid companyId, Guid addressId, CompanyAddressType type
        )
        {
            Check.NotNull(addressId, nameof(addressId));
            Check.NotNull(type, nameof(type));

            var companyAddress = await _companyAddressRepository.GetAsync(id);

            companyAddress.CompanyId = companyId;
            companyAddress.AddressId = addressId;
            companyAddress.Type = type;

            return await _companyAddressRepository.UpdateAsync(companyAddress);
        }

    }
}