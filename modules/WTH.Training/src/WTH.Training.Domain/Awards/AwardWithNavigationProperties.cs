using WTH.Training.AwardTypes;
using WTH.Training.AwardingOrganisations;

using System;
using System.Collections.Generic;

namespace WTH.Training.Awards
{
    public abstract class AwardWithNavigationPropertiesBase
    {
        public Award Award { get; set; } = null!;

        public AwardType AwardType { get; set; } = null!;
        public AwardingOrganisation AwardingOrganisation { get; set; } = null!;
        

        
    }
}