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

namespace WTH.Training.AwardTypes
{
    public abstract class EfCoreAwardTypeRepositoryBase : EfCoreRepository<TrainingDbContext, AwardType, Guid>
    {
        public EfCoreAwardTypeRepositoryBase(IDbContextProvider<TrainingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<AwardType>> GetListAsync(
            string? filterText = null,
            string? name = null,
            bool? hasReferenceNumber = null,
            bool? hasExpiryDate = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, hasReferenceNumber, hasExpiryDate);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AwardTypeConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            bool? hasReferenceNumber = null,
            bool? hasExpiryDate = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, hasReferenceNumber, hasExpiryDate);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<AwardType> ApplyFilter(
            IQueryable<AwardType> query,
            string? filterText = null,
            string? name = null,
            bool? hasReferenceNumber = null,
            bool? hasExpiryDate = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(hasReferenceNumber.HasValue, e => e.HasReferenceNumber == hasReferenceNumber)
                    .WhereIf(hasExpiryDate.HasValue, e => e.HasExpiryDate == hasExpiryDate);
        }
    }
}