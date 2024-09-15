using Volo.Abp.Application.Dtos;
using System;

namespace Wth.Crm.Companies
{
    public abstract class GetCompaniesInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public string? TaxReference { get; set; }
        public Guid? NoteId { get; set; }

        public GetCompaniesInputBase()
        {

        }
    }
}