using Wth.Crm.Employees;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Wth.Crm.EmployeeTelephones
{
    public abstract class EmployeeTelephoneUpdateDtoBase
    {
        public Guid EmployeeId { get; set; }
        [Required]
        public string Value { get; set; } = null!;
        public EmployeeTelephoneType Type { get; set; }

    }
}