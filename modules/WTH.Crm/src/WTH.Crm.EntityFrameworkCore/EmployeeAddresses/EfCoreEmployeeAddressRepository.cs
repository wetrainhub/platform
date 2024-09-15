using Wth.Crm.Employees;
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

namespace Wth.Crm.EmployeeAddresses
{
    public abstract class EfCoreEmployeeAddressRepositoryBase : EfCoreRepository<CrmDbContext, EmployeeAddress, Guid>
    {
        public EfCoreEmployeeAddressRepositoryBase(IDbContextProvider<CrmDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<EmployeeAddress>> GetListByEmployeeIdAsync(
           Guid employeeId,
           string? sorting = null,
           int maxResultCount = int.MaxValue,
           int skipCount = 0,
           CancellationToken cancellationToken = default)
        {
            var query = (await GetQueryableAsync()).Where(x => x.EmployeeId == employeeId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeAddressConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync()).Where(x => x.EmployeeId == employeeId).CountAsync(cancellationToken);
        }

        public virtual async Task<List<EmployeeAddressWithNavigationProperties>> GetListWithNavigationPropertiesByEmployeeIdAsync(
    Guid employeeId,
    string? sorting = null,
    int maxResultCount = int.MaxValue,
    int skipCount = 0,
    CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = query.Where(x => x.EmployeeAddress.EmployeeId == employeeId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeAddressConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<EmployeeAddressWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(employeeAddress => new EmployeeAddressWithNavigationProperties
                {
                    EmployeeAddress = employeeAddress,
                    Address = dbContext.Set<Address>().FirstOrDefault(c => c.Id == employeeAddress.AddressId)
                }).FirstOrDefault();
        }

        public virtual async Task<List<EmployeeAddressWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            EmployeeAddressType? type = null,
            Guid? addressId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, type, addressId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeAddressConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<EmployeeAddressWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from employeeAddress in (await GetDbSetAsync())
                   join address in (await GetDbContextAsync()).Set<Address>() on employeeAddress.AddressId equals address.Id into addresses
                   from address in addresses.DefaultIfEmpty()
                   select new EmployeeAddressWithNavigationProperties
                   {
                       EmployeeAddress = employeeAddress,
                       Address = address
                   };
        }

        protected virtual IQueryable<EmployeeAddressWithNavigationProperties> ApplyFilter(
            IQueryable<EmployeeAddressWithNavigationProperties> query,
            string? filterText,
            EmployeeAddressType? type = null,
            Guid? addressId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(type.HasValue, e => e.EmployeeAddress.Type == type)
                    .WhereIf(addressId != null && addressId != Guid.Empty, e => e.Address != null && e.Address.Id == addressId);
        }

        public virtual async Task<List<EmployeeAddress>> GetListAsync(
            string? filterText = null,
            EmployeeAddressType? type = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, type);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeAddressConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            EmployeeAddressType? type = null,
            Guid? addressId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, type, addressId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<EmployeeAddress> ApplyFilter(
            IQueryable<EmployeeAddress> query,
            string? filterText = null,
            EmployeeAddressType? type = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(type.HasValue, e => e.Type == type);
        }
    }
}