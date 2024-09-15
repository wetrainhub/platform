using Wth.Crm.Companies;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Wth.Crm.CompanyEmails
{
    public partial interface ICompanyEmailRepository : IRepository<CompanyEmail, Guid>
    {
        Task<List<CompanyEmail>> GetListByCompanyIdAsync(
    Guid companyId,
    string? sorting = null,
    int maxResultCount = int.MaxValue,
    int skipCount = 0,
    CancellationToken cancellationToken = default
);

        Task<long> GetCountByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        Task<List<CompanyEmail>> GetListAsync(
                    string? filterText = null,
                    string? value = null,
                    CompanyEmailType? type = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? value = null,
            CompanyEmailType? type = null,
            CancellationToken cancellationToken = default);
    }
}