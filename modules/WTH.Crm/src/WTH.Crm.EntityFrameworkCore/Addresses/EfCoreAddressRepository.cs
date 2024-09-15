using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Wth.Crm.EntityFrameworkCore;

namespace Wth.Crm.Addresses
{
    public class EfCoreAddressRepository : EfCoreRepository<CrmDbContext, Address, Guid>, IAddressRepository
    {
        public EfCoreAddressRepository(IDbContextProvider<CrmDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<Address>> GetListAsync(
            string? filterText = null,
            string? line1 = null,
            string? line2 = null,
            string? line3 = null,
            string? city = null,
            string? county = null,
            string? postcode = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, line1, line2, line3, city, county, postcode);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AddressConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? line1 = null,
            string? line2 = null,
            string? line3 = null,
            string? city = null,
            string? county = null,
            string? postcode = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, line1, line2, line3, city, county, postcode);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Address> ApplyFilter(
            IQueryable<Address> query,
            string? filterText = null,
            string? line1 = null,
            string? line2 = null,
            string? line3 = null,
            string? city = null,
            string? county = null,
            string? postcode = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Line1!.Contains(filterText!) || e.Line2!.Contains(filterText!) || e.Line3!.Contains(filterText!) || e.City!.Contains(filterText!) || e.County!.Contains(filterText!) || e.Postcode!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(line1), e => e.Line1.Contains(line1))
                    .WhereIf(!string.IsNullOrWhiteSpace(line2), e => e.Line2.Contains(line2))
                    .WhereIf(!string.IsNullOrWhiteSpace(line3), e => e.Line3.Contains(line3))
                    .WhereIf(!string.IsNullOrWhiteSpace(city), e => e.City.Contains(city))
                    .WhereIf(!string.IsNullOrWhiteSpace(county), e => e.County.Contains(county))
                    .WhereIf(!string.IsNullOrWhiteSpace(postcode), e => e.Postcode.Contains(postcode));
        }
    }
}