using Wth.Crm.Notes;
using Wth.Crm.Notes;
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

namespace Wth.Crm.Companies
{
    public abstract class EfCoreCompanyRepositoryBase : EfCoreRepository<CrmDbContext, Company, Guid>
    {
        public EfCoreCompanyRepositoryBase(IDbContextProvider<CrmDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<CompanyWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id).Include(x => x.Notes)
                .Select(company => new CompanyWithNavigationProperties
                {
                    Company = company,
                    Notes = (from companyNotes in company.Notes
                             join _note in dbContext.Set<Note>() on companyNotes.NoteId equals _note.Id
                             select _note).ToList()
                }).FirstOrDefault();
        }

        public virtual async Task<List<CompanyWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? name = null,
            string? taxReference = null,
            Guid? noteId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, taxReference, noteId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CompanyConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CompanyWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from company in (await GetDbSetAsync())

                   select new CompanyWithNavigationProperties
                   {
                       Company = company,
                       Notes = new List<Note>()
                   };
        }

        protected virtual IQueryable<CompanyWithNavigationProperties> ApplyFilter(
            IQueryable<CompanyWithNavigationProperties> query,
            string? filterText,
            string? name = null,
            string? taxReference = null,
            Guid? noteId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Company.Name!.Contains(filterText!) || e.Company.TaxReference!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Company.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(taxReference), e => e.Company.TaxReference.Contains(taxReference))
                    .WhereIf(noteId != null && noteId != Guid.Empty, e => e.Company.Notes.Any(x => x.NoteId == noteId));
        }

        public virtual async Task<List<Company>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? taxReference = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, taxReference);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CompanyConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? taxReference = null,
            Guid? noteId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, taxReference, noteId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Company> ApplyFilter(
            IQueryable<Company> query,
            string? filterText = null,
            string? name = null,
            string? taxReference = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.TaxReference!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(taxReference), e => e.TaxReference.Contains(taxReference));
        }
    }
}