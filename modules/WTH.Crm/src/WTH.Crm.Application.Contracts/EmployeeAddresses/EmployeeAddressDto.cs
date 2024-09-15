using Wth.Crm.Employees;
using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;

namespace Wth.Crm.EmployeeAddresses
{
    public abstract class EmployeeAddressDtoBase : FullAuditedEntityDto<Guid>
    {
        public Guid EmployeeId { get; set; }
        public EmployeeAddressType Type { get; set; }
        public Guid AddressId { get; set; }

    }
}