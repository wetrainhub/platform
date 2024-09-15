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

namespace Wth.Crm.CompanyTelephones
{
    public abstract class CompanyTelephoneBase : FullAuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid CompanyId { get; set; }

        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Value { get; set; }

        public virtual CompanyTelephoneType Type { get; set; }

        protected CompanyTelephoneBase()
        {

        }

        public CompanyTelephoneBase(Guid id, Guid companyId, string value, CompanyTelephoneType type)
        {

            Id = id;
            Check.NotNull(value, nameof(value));
            CompanyId = companyId;
            Value = value;
            Type = type;
        }

    }
}