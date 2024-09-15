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

namespace Wth.Crm.EmployeeEmails
{
    public abstract class EmployeeEmailBase : FullAuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid EmployeeId { get; set; }

        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Value { get; set; }

        public virtual EmployeeEmailType Type { get; set; }

        protected EmployeeEmailBase()
        {

        }

        public EmployeeEmailBase(Guid id, Guid employeeId, string value, EmployeeEmailType type)
        {

            Id = id;
            Check.NotNull(value, nameof(value));
            EmployeeId = employeeId;
            Value = value;
            Type = type;
        }

    }
}