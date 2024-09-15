using Wth.Crm.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Wth.Crm.EmployeeAddresses
{
    public abstract class EmployeeAddressManagerBase : DomainService
    {
        protected IEmployeeAddressRepository _employeeAddressRepository;

        public EmployeeAddressManagerBase(IEmployeeAddressRepository employeeAddressRepository)
        {
            _employeeAddressRepository = employeeAddressRepository;
        }

        public virtual async Task<EmployeeAddress> CreateAsync(
        Guid employeeId, Guid addressId, EmployeeAddressType type)
        {
            Check.NotNull(addressId, nameof(addressId));
            Check.NotNull(type, nameof(type));

            var employeeAddress = new EmployeeAddress(
             GuidGenerator.Create(),
             employeeId, addressId, type
             );

            return await _employeeAddressRepository.InsertAsync(employeeAddress);
        }

        public virtual async Task<EmployeeAddress> UpdateAsync(
            Guid id,
            Guid employeeId, Guid addressId, EmployeeAddressType type
        )
        {
            Check.NotNull(addressId, nameof(addressId));
            Check.NotNull(type, nameof(type));

            var employeeAddress = await _employeeAddressRepository.GetAsync(id);

            employeeAddress.EmployeeId = employeeId;
            employeeAddress.AddressId = addressId;
            employeeAddress.Type = type;

            return await _employeeAddressRepository.UpdateAsync(employeeAddress);
        }

    }
}