using Wth.Crm.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Wth.Crm.EmployeeEmails
{
    public abstract class EmployeeEmailManagerBase : DomainService
    {
        protected IEmployeeEmailRepository _employeeEmailRepository;

        public EmployeeEmailManagerBase(IEmployeeEmailRepository employeeEmailRepository)
        {
            _employeeEmailRepository = employeeEmailRepository;
        }

        public virtual async Task<EmployeeEmail> CreateAsync(
        Guid employeeId, string value, EmployeeEmailType type)
        {
            Check.NotNullOrWhiteSpace(value, nameof(value));
            Check.NotNull(type, nameof(type));

            var employeeEmail = new EmployeeEmail(
             GuidGenerator.Create(),
             employeeId, value, type
             );

            return await _employeeEmailRepository.InsertAsync(employeeEmail);
        }

        public virtual async Task<EmployeeEmail> UpdateAsync(
            Guid id,
            Guid employeeId, string value, EmployeeEmailType type
        )
        {
            Check.NotNullOrWhiteSpace(value, nameof(value));
            Check.NotNull(type, nameof(type));

            var employeeEmail = await _employeeEmailRepository.GetAsync(id);

            employeeEmail.EmployeeId = employeeId;
            employeeEmail.Value = value;
            employeeEmail.Type = type;

            return await _employeeEmailRepository.UpdateAsync(employeeEmail);
        }

    }
}