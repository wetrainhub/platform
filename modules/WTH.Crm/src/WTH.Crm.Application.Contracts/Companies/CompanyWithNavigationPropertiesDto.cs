using Wth.Crm.Notes;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace Wth.Crm.Companies
{
    public abstract class CompanyWithNavigationPropertiesDtoBase
    {
        public CompanyDto Company { get; set; } = null!;

        public List<NoteDto> Notes { get; set; } = new List<NoteDto>();

    }
}