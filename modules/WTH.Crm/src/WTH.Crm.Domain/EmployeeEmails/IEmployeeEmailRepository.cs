using Wth.Crm.Employees;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Wth.Crm.EmployeeEmails
{
    public partial interface IEmployeeEmailRepository : IRepository<EmployeeEmail, Guid>
    {
        Task<List<EmployeeEmail>> GetListByEmployeeIdAsync(
    Guid employeeId,
    string? sorting = null,
    int maxResultCount = int.MaxValue,
    int skipCount = 0,
    CancellationToken cancellationToken = default
);

        Task<long> GetCountByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default);

        Task<List<EmployeeEmail>> GetListAsync(
                    string? filterText = null,
                    string? value = null,
                    EmployeeEmailType? type = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? value = null,
            EmployeeEmailType? type = null,
            CancellationToken cancellationToken = default);
    }
}