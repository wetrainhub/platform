using Wth.Crm.Employees;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Wth.Crm.Employees
{
    public abstract class EmployeeUpdateDtoBase : IHasConcurrencyStamp
    {
        [Required]
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; } = null!;
        public string? IdentityNumber { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public EmployeeStatus Status { get; set; }
        public EmployeeType Type { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? EmployeeId { get; set; }
        public List<Guid> NoteIds { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}