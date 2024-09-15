using Wth.Crm.Employees;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Wth.Crm.EmployeeAddresses
{
    public abstract class EmployeeAddressCreateDtoBase
    {
        public Guid EmployeeId { get; set; }
        public EmployeeAddressType Type { get; set; } = ((EmployeeAddressType[])Enum.GetValues(typeof(EmployeeAddressType)))[0];
        public Guid AddressId { get; set; }
    }
}