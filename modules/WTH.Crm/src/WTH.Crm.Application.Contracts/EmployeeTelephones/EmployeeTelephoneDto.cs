using Wth.Crm.Employees;
using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;

namespace Wth.Crm.EmployeeTelephones
{
    public abstract class EmployeeTelephoneDtoBase : FullAuditedEntityDto<Guid>
    {
        public Guid EmployeeId { get; set; }
        public string Value { get; set; } = null!;
        public EmployeeTelephoneType Type { get; set; }

    }
}