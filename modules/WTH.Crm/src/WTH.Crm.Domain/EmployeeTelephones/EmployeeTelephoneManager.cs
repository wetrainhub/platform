using Wth.Crm.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Wth.Crm.EmployeeTelephones
{
    public abstract class EmployeeTelephoneManagerBase : DomainService
    {
        protected IEmployeeTelephoneRepository _employeeTelephoneRepository;

        public EmployeeTelephoneManagerBase(IEmployeeTelephoneRepository employeeTelephoneRepository)
        {
            _employeeTelephoneRepository = employeeTelephoneRepository;
        }

        public virtual async Task<EmployeeTelephone> CreateAsync(
        Guid employeeId, string value, EmployeeTelephoneType type)
        {
            Check.NotNullOrWhiteSpace(value, nameof(value));
            Check.NotNull(type, nameof(type));

            var employeeTelephone = new EmployeeTelephone(
             GuidGenerator.Create(),
             employeeId, value, type
             );

            return await _employeeTelephoneRepository.InsertAsync(employeeTelephone);
        }

        public virtual async Task<EmployeeTelephone> UpdateAsync(
            Guid id,
            Guid employeeId, string value, EmployeeTelephoneType type
        )
        {
            Check.NotNullOrWhiteSpace(value, nameof(value));
            Check.NotNull(type, nameof(type));

            var employeeTelephone = await _employeeTelephoneRepository.GetAsync(id);

            employeeTelephone.EmployeeId = employeeId;
            employeeTelephone.Value = value;
            employeeTelephone.Type = type;

            return await _employeeTelephoneRepository.UpdateAsync(employeeTelephone);
        }

    }
}