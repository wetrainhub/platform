using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using WTH.Training.EntityFrameworkCore;

namespace WTH.Training.AwardingOrganisations
{
    public abstract class EfCoreAwardingOrganisationRepositoryBase : EfCoreRepository<TrainingDbContext, AwardingOrganisation, Guid>
    {
        public EfCoreAwardingOrganisationRepositoryBase(IDbContextProvider<TrainingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<AwardingOrganisation>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? email = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, email);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AwardingOrganisationConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? email = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, email);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<AwardingOrganisation> ApplyFilter(
            IQueryable<AwardingOrganisation> query,
            string? filterText = null,
            string? name = null,
            string? email = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.Email!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(email), e => e.Email.Contains(email));
        }
    }
}