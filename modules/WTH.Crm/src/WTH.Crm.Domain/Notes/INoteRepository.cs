using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Wth.Crm.Notes
{
    public partial interface INoteRepository : IRepository<Note, Guid>
    {
        Task<List<Note>> GetListAsync(
            string? filterText = null,
            string? content = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? content = null,
            CancellationToken cancellationToken = default);
    }
}