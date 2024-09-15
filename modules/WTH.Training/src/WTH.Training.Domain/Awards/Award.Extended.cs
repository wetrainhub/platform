using WTH.Training.AwardTypes;
using WTH.Training.AwardingOrganisations;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

using Volo.Abp;

namespace WTH.Training.Awards
{
    public class Award : AwardBase
    {
        //<suite-custom-code-autogenerated>
        protected Award()
        {

        }

        public Award(Guid id, Guid awardTypeId, Guid awardingOrganisationId, string name, string code, string? description = null)
            : base(id, awardTypeId, awardingOrganisationId, name, code, description)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}