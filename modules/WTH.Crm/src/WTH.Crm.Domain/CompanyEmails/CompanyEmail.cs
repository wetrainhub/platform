using Wth.Crm.Companies;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace Wth.Crm.CompanyEmails
{
    public abstract class CompanyEmailBase : FullAuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid CompanyId { get; set; }

        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Value { get; set; }

        public virtual CompanyEmailType Type { get; set; }

        protected CompanyEmailBase()
        {

        }

        public CompanyEmailBase(Guid id, Guid companyId, string value, CompanyEmailType type)
        {

            Id = id;
            Check.NotNull(value, nameof(value));
            CompanyId = companyId;
            Value = value;
            Type = type;
        }

    }
}