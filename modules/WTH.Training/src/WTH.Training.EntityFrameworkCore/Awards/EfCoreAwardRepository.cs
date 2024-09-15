using WTH.Training.AwardingOrganisations;
using WTH.Training.AwardTypes;
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

namespace WTH.Training.Awards
{
    public abstract class EfCoreAwardRepositoryBase : EfCoreRepository<TrainingDbContext, Award, Guid>
    {
        public EfCoreAwardRepositoryBase(IDbContextProvider<TrainingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<AwardWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(award => new AwardWithNavigationProperties
                {
                    Award = award,
                    AwardType = dbContext.Set<AwardType>().FirstOrDefault(c => c.Id == award.AwardTypeId),
                    AwardingOrganisation = dbContext.Set<AwardingOrganisation>().FirstOrDefault(c => c.Id == award.AwardingOrganisationId)
                }).FirstOrDefault();
        }

        public virtual async Task<List<AwardWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? name = null,
            string? code = null,
            Guid? awardTypeId = null,
            Guid? awardingOrganisationId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, code, awardTypeId, awardingOrganisationId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AwardConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<AwardWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from award in (await GetDbSetAsync())
                   join awardType in (await GetDbContextAsync()).Set<AwardType>() on award.AwardTypeId equals awardType.Id into awardTypes
                   from awardType in awardTypes.DefaultIfEmpty()
                   join awardingOrganisation in (await GetDbContextAsync()).Set<AwardingOrganisation>() on award.AwardingOrganisationId equals awardingOrganisation.Id into awardingOrganisations
                   from awardingOrganisation in awardingOrganisations.DefaultIfEmpty()
                   select new AwardWithNavigationProperties
                   {
                       Award = award,
                       AwardType = awardType,
                       AwardingOrganisation = awardingOrganisation
                   };
        }

        protected virtual IQueryable<AwardWithNavigationProperties> ApplyFilter(
            IQueryable<AwardWithNavigationProperties> query,
            string? filterText,
            string? name = null,
            string? code = null,
            Guid? awardTypeId = null,
            Guid? awardingOrganisationId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Award.Name!.Contains(filterText!) || e.Award.Code!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Award.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Award.Code.Contains(code))
                    .WhereIf(awardTypeId != null && awardTypeId != Guid.Empty, e => e.AwardType != null && e.AwardType.Id == awardTypeId)
                    .WhereIf(awardingOrganisationId != null && awardingOrganisationId != Guid.Empty, e => e.AwardingOrganisation != null && e.AwardingOrganisation.Id == awardingOrganisationId);
        }

        public virtual async Task<List<Award>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? code = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, code);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AwardConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? code = null,
            Guid? awardTypeId = null,
            Guid? awardingOrganisationId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, code, awardTypeId, awardingOrganisationId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Award> ApplyFilter(
            IQueryable<Award> query,
            string? filterText = null,
            string? name = null,
            string? code = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.Code!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code));
        }
    }
}