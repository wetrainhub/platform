using Wth.Crm.Employees;
using Wth.Crm.Companies;
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

namespace Wth.Crm.Employees
{
    public abstract class EfCoreEmployeeRepositoryBase : EfCoreRepository<CrmDbContext, Employee, Guid>
    {
        public EfCoreEmployeeRepositoryBase(IDbContextProvider<CrmDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<EmployeeWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id).Include(x => x.Notes)
                .Select(employee => new EmployeeWithNavigationProperties
                {
                    Employee = employee,
                    Company = dbContext.Set<Company>().FirstOrDefault(c => c.Id == employee.CompanyId),
                    Employee1 = dbContext.Set<Employee>().FirstOrDefault(c => c.Id == employee.EmployeeId),
                    Notes = (from employeeNotes in employee.Notes
                             join _note in dbContext.Set<Note>() on employeeNotes.NoteId equals _note.Id
                             select _note).ToList()
                }).FirstOrDefault();
        }

        public virtual async Task<List<EmployeeWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? firstName = null,
            string? lastName = null,
            string? identityNumber = null,
            string? enrolmentNumber = null,
            EmployeeStatus? status = null,
            EmployeeType? type = null,
            Guid? companyId = null,
            Guid? employeeId = null,
            Guid? noteId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, firstName, lastName, identityNumber, enrolmentNumber, status, type, companyId, employeeId, noteId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<EmployeeWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from employee in (await GetDbSetAsync())
                   join company in (await GetDbContextAsync()).Set<Company>() on employee.CompanyId equals company.Id into companies
                   from company in companies.DefaultIfEmpty()
                   join employee1 in (await GetDbContextAsync()).Set<Employee>() on employee.EmployeeId equals employee1.Id into employees1
                   from employee1 in employees1.DefaultIfEmpty()
                   select new EmployeeWithNavigationProperties
                   {
                       Employee = employee,
                       Company = company,
                       Employee1 = employee1,
                       Notes = new List<Note>()
                   };
        }

        protected virtual IQueryable<EmployeeWithNavigationProperties> ApplyFilter(
            IQueryable<EmployeeWithNavigationProperties> query,
            string? filterText,
            string? firstName = null,
            string? lastName = null,
            string? identityNumber = null,
            string? enrolmentNumber = null,
            EmployeeStatus? status = null,
            EmployeeType? type = null,
            Guid? companyId = null,
            Guid? employeeId = null,
            Guid? noteId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Employee.FirstName!.Contains(filterText!) || e.Employee.LastName!.Contains(filterText!) || e.Employee.IdentityNumber!.Contains(filterText!) || e.Employee.EnrolmentNumber!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.Employee.FirstName.Contains(firstName))
                    .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.Employee.LastName.Contains(lastName))
                    .WhereIf(!string.IsNullOrWhiteSpace(identityNumber), e => e.Employee.IdentityNumber.Contains(identityNumber))
                    .WhereIf(!string.IsNullOrWhiteSpace(enrolmentNumber), e => e.Employee.EnrolmentNumber.Contains(enrolmentNumber))
                    .WhereIf(status.HasValue, e => e.Employee.Status == status)
                    .WhereIf(type.HasValue, e => e.Employee.Type == type)
                    .WhereIf(companyId != null && companyId != Guid.Empty, e => e.Company != null && e.Company.Id == companyId)
                    .WhereIf(employeeId != null && employeeId != Guid.Empty, e => e.Employee1 != null && e.Employee1.Id == employeeId)
                    .WhereIf(noteId != null && noteId != Guid.Empty, e => e.Employee.Notes.Any(x => x.NoteId == noteId));
        }

        public virtual async Task<List<Employee>> GetListAsync(
            string? filterText = null,
            string? firstName = null,
            string? lastName = null,
            string? identityNumber = null,
            string? enrolmentNumber = null,
            EmployeeStatus? status = null,
            EmployeeType? type = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, firstName, lastName, identityNumber, enrolmentNumber, status, type);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? firstName = null,
            string? lastName = null,
            string? identityNumber = null,
            string? enrolmentNumber = null,
            EmployeeStatus? status = null,
            EmployeeType? type = null,
            Guid? companyId = null,
            Guid? employeeId = null,
            Guid? noteId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, firstName, lastName, identityNumber, enrolmentNumber, status, type, companyId, employeeId, noteId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Employee> ApplyFilter(
            IQueryable<Employee> query,
            string? filterText = null,
            string? firstName = null,
            string? lastName = null,
            string? identityNumber = null,
            string? enrolmentNumber = null,
            EmployeeStatus? status = null,
            EmployeeType? type = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.FirstName!.Contains(filterText!) || e.LastName!.Contains(filterText!) || e.IdentityNumber!.Contains(filterText!) || e.EnrolmentNumber!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.FirstName.Contains(firstName))
                    .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.LastName.Contains(lastName))
                    .WhereIf(!string.IsNullOrWhiteSpace(identityNumber), e => e.IdentityNumber.Contains(identityNumber))
                    .WhereIf(!string.IsNullOrWhiteSpace(enrolmentNumber), e => e.EnrolmentNumber.Contains(enrolmentNumber))
                    .WhereIf(status.HasValue, e => e.Status == status)
                    .WhereIf(type.HasValue, e => e.Type == type);
        }
    }
}