using Wth.Crm.Employees;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Wth.Crm.EmployeeAddresses
{
    public partial interface IEmployeeAddressRepository : IRepository<EmployeeAddress, Guid>
    {
        Task<List<EmployeeAddress>> GetListByEmployeeIdAsync(
    Guid employeeId,
    string? sorting = null,
    int maxResultCount = int.MaxValue,
    int skipCount = 0,
    CancellationToken cancellationToken = default
);

        Task<long> GetCountByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default);

        Task<List<EmployeeAddressWithNavigationProperties>> GetListWithNavigationPropertiesByEmployeeIdAsync(
            Guid employeeId,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<EmployeeAddressWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken = default
        );

        Task<List<EmployeeAddressWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            EmployeeAddressType? type = null,
            Guid? addressId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<EmployeeAddress>> GetListAsync(
                    string? filterText = null,
                    EmployeeAddressType? type = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            EmployeeAddressType? type = null,
            Guid? addressId = null,
            CancellationToken cancellationToken = default);
    }
}