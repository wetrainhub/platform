using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Wth.Crm.CompanyAddresses;
using Wth.Crm.CompanyEmails;
using Wth.Crm.CompanyTelephones;

using Volo.Abp;

namespace Wth.Crm.Companies
{
    public abstract class CompanyBase : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        [CanBeNull]
        public virtual string? TaxReference { get; set; }

        public ICollection<CompanyNote> Notes { get; private set; }
        public ICollection<CompanyAddress> CompanyAddresses { get; private set; }
        public ICollection<CompanyEmail> CompanyEmails { get; private set; }
        public ICollection<CompanyTelephone> CompanyTelephones { get; private set; }

        protected CompanyBase()
        {

        }

        public CompanyBase(Guid id, string name, string? taxReference = null)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Name = name;
            TaxReference = taxReference;
            Notes = new Collection<CompanyNote>();
            CompanyAddresses = new Collection<CompanyAddress>();
            CompanyEmails = new Collection<CompanyEmail>();
            CompanyTelephones = new Collection<CompanyTelephone>();
        }
        public virtual void AddNote(Guid noteId)
        {
            Check.NotNull(noteId, nameof(noteId));

            if (IsInNotes(noteId))
            {
                return;
            }

            Notes.Add(new CompanyNote(Id, noteId));
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
            Notes.RemoveAll(x => x.CompanyId == Id);
        }

        private bool IsInNotes(Guid noteId)
        {
            return Notes.Any(x => x.NoteId == noteId);
        }
    }
}