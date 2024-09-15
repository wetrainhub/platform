using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace WTH.Training.AwardingOrganisations
{
    public partial interface IAwardingOrganisationRepository : IRepository<AwardingOrganisation, Guid>
    {
        Task<List<AwardingOrganisation>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? email = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? email = null,
            CancellationToken cancellationToken = default);
    }
}