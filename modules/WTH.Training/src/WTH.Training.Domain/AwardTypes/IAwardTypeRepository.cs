using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace WTH.Training.AwardTypes
{
    public partial interface IAwardTypeRepository : IRepository<AwardType, Guid>
    {
        Task<List<AwardType>> GetListAsync(
            string? filterText = null,
            string? name = null,
            bool? hasReferenceNumber = null,
            bool? hasExpiryDate = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            bool? hasReferenceNumber = null,
            bool? hasExpiryDate = null,
            CancellationToken cancellationToken = default);
    }
}