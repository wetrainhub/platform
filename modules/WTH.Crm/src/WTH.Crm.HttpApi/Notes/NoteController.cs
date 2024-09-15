using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Wth.Crm.Notes;

namespace Wth.Crm.Notes
{
    [RemoteService(Name = "Crm")]
    [Area("crm")]
    [ControllerName("Note")]
    [Route("api/crm/notes")]
    public abstract class NoteControllerBase : AbpController
    {
        protected INotesAppService _notesAppService;

        public NoteControllerBase(INotesAppService notesAppService)
        {
            _notesAppService = notesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<NoteDto>> GetListAsync(GetNotesInput input)
        {
            return _notesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<NoteDto> GetAsync(Guid id)
        {
            return _notesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<NoteDto> CreateAsync(NoteCreateDto input)
        {
            return _notesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<NoteDto> UpdateAsync(Guid id, NoteUpdateDto input)
        {
            return _notesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _notesAppService.DeleteAsync(id);
        }
    }
}