using Wth.Crm.Employees;
using System;
using System.Collections.Generic;
using Wth.Crm.EmployeeEmails;
using Wth.Crm.EmployeeTelephones;
using Wth.Crm.EmployeeAddresses;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Wth.Crm.Employees
{
    public abstract class EmployeeDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string? IdentityNumber { get; set; }
        public string? EnrolmentNumber { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public EmployeeStatus Status { get; set; }
        public EmployeeType Type { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? EmployeeId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

        public List<EmployeeEmailDto> EmployeeEmails { get; set; } = new();
        public List<EmployeeTelephoneDto> EmployeeTelephones { get; set; } = new();
        public List<EmployeeAddressWithNavigationPropertiesDto> EmployeeAddresses { get; set; } = new();
    }
}