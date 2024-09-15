using Wth.Crm.Employees;
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

namespace Wth.Crm.EmployeeAddresses
{
    public abstract class EmployeeAddressBase : FullAuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid EmployeeId { get; set; }

        public virtual Guid? TenantId { get; set; }

        public virtual EmployeeAddressType Type { get; set; }
        public Guid AddressId { get; set; }

        protected EmployeeAddressBase()
        {

        }

        public EmployeeAddressBase(Guid id, Guid employeeId, Guid addressId, EmployeeAddressType type)
        {

            Id = id;
            EmployeeId = employeeId;
            Type = type;
            AddressId = addressId;
        }

    }
}