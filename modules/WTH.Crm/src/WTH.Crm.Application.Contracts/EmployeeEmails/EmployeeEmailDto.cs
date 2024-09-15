using Wth.Crm.Employees;
using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;

namespace Wth.Crm.EmployeeEmails
{
    public abstract class EmployeeEmailDtoBase : FullAuditedEntityDto<Guid>
    {
        public Guid EmployeeId { get; set; }
        public string Value { get; set; } = null!;
        public EmployeeEmailType Type { get; set; }

    }
}