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
    public abstract class AwardBase : FullAuditedEntity<Guid>, IMultiTenant, IHasConcurrencyStamp
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        [CanBeNull]
        public virtual string? Description { get; set; }

        [NotNull]
        public virtual string Code { get; set; }
        public Guid AwardTypeId { get; set; }
        public Guid AwardingOrganisationId { get; set; }

        public string ConcurrencyStamp { get; set; }

        protected AwardBase()
        {

        }

        public AwardBase(Guid id, Guid awardTypeId, Guid awardingOrganisationId, string name, string code, string? description = null)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            Id = id;
            Check.NotNull(name, nameof(name));
            Check.NotNull(code, nameof(code));
            Name = name;
            Code = code;
            Description = description;
            AwardTypeId = awardTypeId;
            AwardingOrganisationId = awardingOrganisationId;
        }

    }
}