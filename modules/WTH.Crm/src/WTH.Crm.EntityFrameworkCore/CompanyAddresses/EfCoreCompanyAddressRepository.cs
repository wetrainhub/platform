using Wth.Crm.Companies;
using Wth.Crm.Addresses;
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

namespace Wth.Crm.CompanyAddresses
{
    public abstract class EfCoreCompanyAddressRepositoryBase : EfCoreRepository<CrmDbContext, CompanyAddress, Guid>
    {
        public EfCoreCompanyAddressRepositoryBase(IDbContextProvider<CrmDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<CompanyAddress>> GetListByCompanyIdAsync(
           Guid companyId,
           string? sorting = null,
           int maxResultCount = int.MaxValue,
           int skipCount = 0,
           CancellationToken cancellationToken = default)
        {
            var query = (await GetQueryableAsync()).Where(x => x.CompanyId == companyId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CompanyAddressConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync()).Where(x => x.CompanyId == companyId).CountAsync(cancellationToken);
        }

        public virtual async Task<List<CompanyAddressWithNavigationProperties>> GetListWithNavigationPropertiesByCompanyIdAsync(
    Guid companyId,
    string? sorting = null,
    int maxResultCount = int.MaxValue,
    int skipCount = 0,
    CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = query.Where(x => x.CompanyAddress.CompanyId == companyId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CompanyAddressConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<CompanyAddressWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(companyAddress => new CompanyAddressWithNavigationProperties
                {
                    CompanyAddress = companyAddress,
                    Address = dbContext.Set<Address>().FirstOrDefault(c => c.Id == companyAddress.AddressId)
                }).FirstOrDefault();
        }

        public virtual async Task<List<CompanyAddressWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            CompanyAddressType? type = null,
            Guid? addressId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, type, addressId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CompanyAddressConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CompanyAddressWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from companyAddress in (await GetDbSetAsync())
                   join address in (await GetDbContextAsync()).Set<Address>() on companyAddress.AddressId equals address.Id into addresses
                   from address in addresses.DefaultIfEmpty()
                   select new CompanyAddressWithNavigationProperties
                   {
                       CompanyAddress = companyAddress,
                       Address = address
                   };
        }

        protected virtual IQueryable<CompanyAddressWithNavigationProperties> ApplyFilter(
            IQueryable<CompanyAddressWithNavigationProperties> query,
            string? filterText,
            CompanyAddressType? type = null,
            Guid? addressId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(type.HasValue, e => e.CompanyAddress.Type == type)
                    .WhereIf(addressId != null && addressId != Guid.Empty, e => e.Address != null && e.Address.Id == addressId);
        }

        public virtual async Task<List<CompanyAddress>> GetListAsync(
            string? filterText = null,
            CompanyAddressType? type = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, type);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CompanyAddressConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            CompanyAddressType? type = null,
            Guid? addressId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, type, addressId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CompanyAddress> ApplyFilter(
            IQueryable<CompanyAddress> query,
            string? filterText = null,
            CompanyAddressType? type = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(type.HasValue, e => e.Type == type);
        }
    }
}