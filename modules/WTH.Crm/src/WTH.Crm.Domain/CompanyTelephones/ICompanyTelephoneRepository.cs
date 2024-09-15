using Wth.Crm.Companies;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Wth.Crm.CompanyTelephones
{
    public partial interface ICompanyTelephoneRepository : IRepository<CompanyTelephone, Guid>
    {
        Task<List<CompanyTelephone>> GetListByCompanyIdAsync(
    Guid companyId,
    string? sorting = null,
    int maxResultCount = int.MaxValue,
    int skipCount = 0,
    CancellationToken cancellationToken = default
);

        Task<long> GetCountByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        Task<List<CompanyTelephone>> GetListAsync(
                    string? filterText = null,
                    string? value = null,
                    CompanyTelephoneType? type = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? value = null,
            CompanyTelephoneType? type = null,
            CancellationToken cancellationToken = default);
    }
}