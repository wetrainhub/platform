using Wth.Crm.Employees;
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

namespace Wth.Crm.EmployeeEmails
{
    public abstract class EfCoreEmployeeEmailRepositoryBase : EfCoreRepository<CrmDbContext, EmployeeEmail, Guid>
    {
        public EfCoreEmployeeEmailRepositoryBase(IDbContextProvider<CrmDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<EmployeeEmail>> GetListByEmployeeIdAsync(
           Guid employeeId,
           string? sorting = null,
           int maxResultCount = int.MaxValue,
           int skipCount = 0,
           CancellationToken cancellationToken = default)
        {
            var query = (await GetQueryableAsync()).Where(x => x.EmployeeId == employeeId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeEmailConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync()).Where(x => x.EmployeeId == employeeId).CountAsync(cancellationToken);
        }

        public virtual async Task<List<EmployeeEmail>> GetListAsync(
            string? filterText = null,
            string? value = null,
            EmployeeEmailType? type = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, value, type);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeEmailConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? value = null,
            EmployeeEmailType? type = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, value, type);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<EmployeeEmail> ApplyFilter(
            IQueryable<EmployeeEmail> query,
            string? filterText = null,
            string? value = null,
            EmployeeEmailType? type = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Value!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(value), e => e.Value.Contains(value))
                    .WhereIf(type.HasValue, e => e.Type == type);
        }
    }
}