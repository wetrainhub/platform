using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace WTH.Training.AwardingOrganisations
{
    public abstract class AwardingOrganisationBase : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        [CanBeNull]
        public virtual string? Email { get; set; }

        [CanBeNull]
        public virtual string? Telephone { get; set; }

        protected AwardingOrganisationBase()
        {

        }

        public AwardingOrganisationBase(Guid id, string name, string? email = null, string? telephone = null)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Name = name;
            Email = email;
            Telephone = telephone;
        }

    }
}