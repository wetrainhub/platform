using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace WTH.Training.Awards
{
    public partial interface IAwardRepository : IRepository<Award, Guid>
    {
        Task<AwardWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<AwardWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? name = null,
            string? code = null,
            Guid? awardTypeId = null,
            Guid? awardingOrganisationId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Award>> GetListAsync(
                    string? filterText = null,
                    string? name = null,
                    string? code = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? code = null,
            Guid? awardTypeId = null,
            Guid? awardingOrganisationId = null,
            CancellationToken cancellationToken = default);
    }
}