using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Wth.Crm.Notes
{
    public partial interface INotesAppService : IApplicationService
    {

        Task<PagedResultDto<NoteDto>> GetListAsync(GetNotesInput input);

        Task<NoteDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<NoteDto> CreateAsync(NoteCreateDto input);

        Task<NoteDto> UpdateAsync(Guid id, NoteUpdateDto input);
    }
}