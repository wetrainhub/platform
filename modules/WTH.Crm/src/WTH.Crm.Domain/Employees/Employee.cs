using Wth.Crm.Employees;
using Wth.Crm.Companies;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Wth.Crm.EmployeeEmails;
using Wth.Crm.EmployeeTelephones;
using Wth.Crm.EmployeeAddresses;

using Volo.Abp;

namespace Wth.Crm.Employees
{
    public abstract class EmployeeBase : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string FirstName { get; set; }

        [CanBeNull]
        public virtual string? MiddleName { get; set; }

        [NotNull]
        public virtual string LastName { get; set; }

        [CanBeNull]
        public virtual string? IdentityNumber { get; set; }

        [CanBeNull]
        public virtual string? EnrolmentNumber { get; set; }

        public virtual DateOnly? DateOfBirth { get; set; }

        public virtual EmployeeStatus Status { get; set; }

        public virtual EmployeeType Type { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? EmployeeId { get; set; }
        public ICollection<EmployeeNote> Notes { get; private set; }
        public ICollection<EmployeeEmail> EmployeeEmails { get; private set; }
        public ICollection<EmployeeTelephone> EmployeeTelephones { get; private set; }
        public ICollection<EmployeeAddress> EmployeeAddresses { get; private set; }

        protected EmployeeBase()
        {

        }

        public EmployeeBase(Guid id, Guid? companyId, Guid? employeeId, string firstName, string lastName, EmployeeStatus status, EmployeeType type, string? middleName = null, string? identityNumber = null, string? enrolmentNumber = null, DateOnly? dateOfBirth = null)
        {

            Id = id;
            Check.NotNull(firstName, nameof(firstName));
            Check.NotNull(lastName, nameof(lastName));
            FirstName = firstName;
            LastName = lastName;
            Status = status;
            Type = type;
            MiddleName = middleName;
            IdentityNumber = identityNumber;
            EnrolmentNumber = enrolmentNumber;
            DateOfBirth = dateOfBirth;
            CompanyId = companyId;
            EmployeeId = employeeId;
            Notes = new Collection<EmployeeNote>();
            EmployeeEmails = new Collection<EmployeeEmail>();
            EmployeeTelephones = new Collection<EmployeeTelephone>();
            EmployeeAddresses = new Collection<EmployeeAddress>();
        }
        public virtual void AddNote(Guid noteId)
        {
            Check.NotNull(noteId, nameof(noteId));

            if (IsInNotes(noteId))
            {
                return;
            }

            Notes.Add(new EmployeeNote(Id, noteId));
        }

        public virtual void RemoveNote(Guid noteId)
        {
            Check.NotNull(noteId, nameof(noteId));

            if (!IsInNotes(noteId))
            {
                return;
            }

            Notes.RemoveAll(x => x.NoteId == noteId);
        }

        public virtual void RemoveAllNotesExceptGivenIds(List<Guid> noteIds)
        {
            Check.NotNullOrEmpty(noteIds, nameof(noteIds));

            Notes.RemoveAll(x => !noteIds.Contains(x.NoteId));
        }

        public virtual void RemoveAllNotes()
        {
            Notes.RemoveAll(x => x.EmployeeId == Id);
        }

        private bool IsInNotes(Guid noteId)
        {
            return Notes.Any(x => x.NoteId == noteId);
        }
    }
}