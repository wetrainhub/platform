using Wth.Crm.Employees;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace Wth.Crm.EmployeeTelephones
{
    public abstract class EmployeeTelephoneBase : FullAuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid EmployeeId { get; set; }

        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Value { get; set; }

        public virtual EmployeeTelephoneType Type { get; set; }

        protected EmployeeTelephoneBase()
        {

        }

        public EmployeeTelephoneBase(Guid id, Guid employeeId, string value, EmployeeTelephoneType type)
        {

            Id = id;
            Check.NotNull(value, nameof(value));
            EmployeeId = employeeId;
            Value = value;
            Type = type;
        }

    }
}