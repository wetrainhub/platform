using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace Wth.Crm.Notes
{
    public abstract class NoteManagerBase : DomainService
    {
        protected INoteRepository _noteRepository;

        public NoteManagerBase(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public virtual async Task<Note> CreateAsync(
        string content)
        {
            Check.NotNullOrWhiteSpace(content, nameof(content));

            var note = new Note(
             GuidGenerator.Create(),
             content
             );

            return await _noteRepository.InsertAsync(note);
        }

        public virtual async Task<Note> UpdateAsync(
            Guid id,
            string content, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(content, nameof(content));

            var note = await _noteRepository.GetAsync(id);

            note.Content = content;

            note.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _noteRepository.UpdateAsync(note);
        }

    }
}