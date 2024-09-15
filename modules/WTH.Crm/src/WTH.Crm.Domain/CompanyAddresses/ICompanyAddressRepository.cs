using Wth.Crm.Companies;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Wth.Crm.CompanyAddresses
{
    public partial interface ICompanyAddressRepository : IRepository<CompanyAddress, Guid>
    {
        Task<List<CompanyAddress>> GetListByCompanyIdAsync(
    Guid companyId,
    string? sorting = null,
    int maxResultCount = int.MaxValue,
    int skipCount = 0,
    CancellationToken cancellationToken = default
);

        Task<long> GetCountByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        Task<List<CompanyAddressWithNavigationProperties>> GetListWithNavigationPropertiesByCompanyIdAsync(
            Guid companyId,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<CompanyAddressWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken = default
        );

        Task<List<CompanyAddressWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            CompanyAddressType? type = null,
            Guid? addressId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<CompanyAddress>> GetListAsync(
                    string? filterText = null,
                    CompanyAddressType? type = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            CompanyAddressType? type = null,
            Guid? addressId = null,
            CancellationToken cancellationToken = default);
    }
}