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

namespace WTH.Training.AwardTypes
{
    public abstract class AwardTypeBase : FullAuditedEntity<Guid>, IMultiTenant, IHasConcurrencyStamp
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        public virtual bool HasReferenceNumber { get; set; }

        public virtual bool HasExpiryDate { get; set; }

        public string ConcurrencyStamp { get; set; }

        protected AwardTypeBase()
        {

        }

        public AwardTypeBase(Guid id, string name, bool hasReferenceNumber, bool hasExpiryDate)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            Id = id;
            Check.NotNull(name, nameof(name));
            Name = name;
            HasReferenceNumber = hasReferenceNumber;
            HasExpiryDate = hasExpiryDate;
        }

    }
}