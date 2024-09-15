using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Wth.Crm.Permissions;
using Wth.Crm.Notes;

namespace Wth.Crm.Notes
{

    [Authorize(CrmPermissions.Notes.Default)]
    public abstract class NotesAppServiceBase : CrmAppService
    {

        protected INoteRepository _noteRepository;
        protected NoteManager _noteManager;

        public NotesAppServiceBase(INoteRepository noteRepository, NoteManager noteManager)
        {

            _noteRepository = noteRepository;
            _noteManager = noteManager;

        }

        public virtual async Task<PagedResultDto<NoteDto>> GetListAsync(GetNotesInput input)
        {
            var totalCount = await _noteRepository.GetCountAsync(input.FilterText, input.Content);
            var items = await _noteRepository.GetListAsync(input.FilterText, input.Content, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<NoteDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Note>, List<NoteDto>>(items)
            };
        }

        public virtual async Task<NoteDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Note, NoteDto>(await _noteRepository.GetAsync(id));
        }

        [Authorize(CrmPermissions.Notes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _noteRepository.DeleteAsync(id);
        }

        [Authorize(CrmPermissions.Notes.Create)]
        public virtual async Task<NoteDto> CreateAsync(NoteCreateDto input)
        {

            var note = await _noteManager.CreateAsync(
            input.Content
            );

            return ObjectMapper.Map<Note, NoteDto>(note);
        }

        [Authorize(CrmPermissions.Notes.Edit)]
        public virtual async Task<NoteDto> UpdateAsync(Guid id, NoteUpdateDto input)
        {

            var note = await _noteManager.UpdateAsync(
            id,
            input.Content, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Note, NoteDto>(note);
        }
    }
}