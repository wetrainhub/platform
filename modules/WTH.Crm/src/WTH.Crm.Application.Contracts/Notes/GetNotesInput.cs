using Volo.Abp.Application.Dtos;
using System;

namespace Wth.Crm.Notes
{
    public abstract class GetNotesInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? Content { get; set; }

        public GetNotesInputBase()
        {

        }
    }
}