using Wth.Crm.Employees;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Wth.Crm.EmployeeAddresses
{
    public abstract class EmployeeAddressUpdateDtoBase
    {
        public Guid EmployeeId { get; set; }
        public EmployeeAddressType Type { get; set; }
        public Guid AddressId { get; set; }

    }
}