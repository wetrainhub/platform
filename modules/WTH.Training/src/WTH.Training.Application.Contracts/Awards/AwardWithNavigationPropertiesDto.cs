using WTH.Training.AwardTypes;
using WTH.Training.AwardingOrganisations;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace WTH.Training.Awards
{
    public abstract class AwardWithNavigationPropertiesDtoBase
    {
        public AwardDto Award { get; set; } = null!;

        public AwardTypeDto AwardType { get; set; } = null!;
        public AwardingOrganisationDto AwardingOrganisation { get; set; } = null!;

    }
}