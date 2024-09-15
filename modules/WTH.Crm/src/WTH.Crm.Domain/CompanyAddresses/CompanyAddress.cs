using Wth.Crm.Companies;
using Wth.Crm.Addresses;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace Wth.Crm.CompanyAddresses
{
    public abstract class CompanyAddressBase : FullAuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid CompanyId { get; set; }

        public virtual Guid? TenantId { get; set; }

        public virtual CompanyAddressType Type { get; set; }
        public Guid AddressId { get; set; }

        protected CompanyAddressBase()
        {

        }

        public CompanyAddressBase(Guid id, Guid companyId, Guid addressId, CompanyAddressType type)
        {

            Id = id;
            CompanyId = companyId;
            Type = type;
            AddressId = addressId;
        }

    }
}