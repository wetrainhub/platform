using Volo.Abp.Application.Dtos;
using System;

namespace WTH.Training.AwardingOrganisations
{
    public abstract class GetAwardingOrganisationsInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public string? Email { get; set; }

        public GetAwardingOrganisationsInputBase()
        {

        }
    }
}