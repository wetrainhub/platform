using Wth.Crm.Employees;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Wth.Crm.Employees
{
    public abstract class EmployeeCreateDtoBase
    {
        [Required]
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; } = null!;
        public string? IdentityNumber { get; set; }
        public string? EnrolmentNumber { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public EmployeeStatus Status { get; set; } = ((EmployeeStatus[])Enum.GetValues(typeof(EmployeeStatus)))[0];
        public EmployeeType Type { get; set; } = ((EmployeeType[])Enum.GetValues(typeof(EmployeeType)))[0];
        public Guid? CompanyId { get; set; }
        public Guid? EmployeeId { get; set; }
        public List<Guid> NoteIds { get; set; }
    }
}