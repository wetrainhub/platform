using Wth.Crm.Notes;
using Wth.Crm.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace Wth.Crm.Employees
{
    public abstract class EmployeeManagerBase : DomainService
    {
        protected IEmployeeRepository _employeeRepository;
        protected IRepository<Note, Guid> _noteRepository;

        public EmployeeManagerBase(IEmployeeRepository employeeRepository,
        IRepository<Note, Guid> noteRepository)
        {
            _employeeRepository = employeeRepository;
            _noteRepository = noteRepository;
        }

        public virtual async Task<Employee> CreateAsync(
        List<Guid> noteIds,
        Guid? companyId, Guid? employeeId, string firstName, string lastName, EmployeeStatus status, EmployeeType type, string? middleName = null, string? identityNumber = null, string? enrolmentNumber = null, DateOnly? dateOfBirth = null)
        {
            Check.NotNullOrWhiteSpace(firstName, nameof(firstName));
            Check.NotNullOrWhiteSpace(lastName, nameof(lastName));
            Check.NotNull(status, nameof(status));
            Check.NotNull(type, nameof(type));

            var employee = new Employee(
             GuidGenerator.Create(),
             companyId, employeeId, firstName, lastName, status, type, middleName, identityNumber, enrolmentNumber, dateOfBirth
             );

            await SetNotesAsync(employee, noteIds);

            return await _employeeRepository.InsertAsync(employee);
        }

        public virtual async Task<Employee> UpdateAsync(
            Guid id,
            List<Guid> noteIds,
        Guid? companyId, Guid? employeeId, string firstName, string lastName, EmployeeStatus status, EmployeeType type, string? middleName = null, string? identityNumber = null, DateOnly? dateOfBirth = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(firstName, nameof(firstName));
            Check.NotNullOrWhiteSpace(lastName, nameof(lastName));
            Check.NotNull(status, nameof(status));
            Check.NotNull(type, nameof(type));

            var queryable = await _employeeRepository.WithDetailsAsync(x => x.Notes);
            var query = queryable.Where(x => x.Id == id);

            var employee = await AsyncExecuter.FirstOrDefaultAsync(query);

            employee.CompanyId = companyId;
            employee.EmployeeId = employeeId;
            employee.FirstName = firstName;
            employee.LastName = lastName;
            employee.Status = status;
            employee.Type = type;
            employee.MiddleName = middleName;
            employee.IdentityNumber = identityNumber;
            employee.DateOfBirth = dateOfBirth;

            await SetNotesAsync(employee, noteIds);

            employee.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _employeeRepository.UpdateAsync(employee);
        }

        private async Task SetNotesAsync(Employee employee, List<Guid> noteIds)
        {
            if (noteIds == null || !noteIds.Any())
            {
                employee.RemoveAllNotes();
                return;
            }

            var query = (await _noteRepository.GetQueryableAsync())
                .Where(x => noteIds.Contains(x.Id))
                .Select(x => x.Id);

            var noteIdsInDb = await AsyncExecuter.ToListAsync(query);
            if (!noteIdsInDb.Any())
            {
                return;
            }

            employee.RemoveAllNotesExceptGivenIds(noteIdsInDb);

            foreach (var noteId in noteIdsInDb)
            {
                employee.AddNote(noteId);
            }
        }

    }
}