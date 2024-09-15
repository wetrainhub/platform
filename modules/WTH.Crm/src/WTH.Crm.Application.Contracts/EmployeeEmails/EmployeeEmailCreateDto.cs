using Wth.Crm.Employees;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Wth.Crm.EmployeeEmails
{
    public abstract class EmployeeEmailCreateDtoBase
    {
        public Guid EmployeeId { get; set; }
        [Required]
        [EmailAddress]
        public string Value { get; set; } = null!;
        public EmployeeEmailType Type { get; set; } = ((EmployeeEmailType[])Enum.GetValues(typeof(EmployeeEmailType)))[0];
    }
}