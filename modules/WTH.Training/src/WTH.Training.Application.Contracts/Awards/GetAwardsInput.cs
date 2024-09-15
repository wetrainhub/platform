using Volo.Abp.Application.Dtos;
using System;

namespace WTH.Training.Awards
{
    public abstract class GetAwardsInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public string? Code { get; set; }
        public Guid? AwardTypeId { get; set; }
        public Guid? AwardingOrganisationId { get; set; }

        public GetAwardsInputBase()
        {

        }
    }
}