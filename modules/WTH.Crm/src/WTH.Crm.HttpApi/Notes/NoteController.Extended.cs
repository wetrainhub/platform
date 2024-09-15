using Asp.Versioning;
using System;
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
    public class NoteController : NoteControllerBase, INotesAppService
    {
        public NoteController(INotesAppService notesAppService) : base(notesAppService)
        {
        }
    }
}