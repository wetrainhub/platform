using Wth.Crm.Employees;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Wth.Crm.Employees
{
    public partial interface IEmployeeRepository : IRepository<Employee, Guid>
    {
        Task<EmployeeWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<EmployeeWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? firstName = null,
            string? lastName = null,
            string? identityNumber = null,
            string? enrolmentNumber = null,
            EmployeeStatus? status = null,
            EmployeeType? type = null,
            Guid? companyId = null,
            Guid? employeeId = null,
            Guid? noteId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Employee>> GetListAsync(
                    string? filterText = null,
                    string? firstName = null,
                    string? lastName = null,
                    string? identityNumber = null,
                    string? enrolmentNumber = null,
                    EmployeeStatus? status = null,
                    EmployeeType? type = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? firstName = null,
            string? lastName = null,
            string? identityNumber = null,
            string? enrolmentNumber = null,
            EmployeeStatus? status = null,
            EmployeeType? type = null,
            Guid? companyId = null,
            Guid? employeeId = null,
            Guid? noteId = null,
            CancellationToken cancellationToken = default);
    }
}