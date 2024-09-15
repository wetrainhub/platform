using Wth.Crm.Employees;
using Volo.Abp.Application.Dtos;
using System;

namespace Wth.Crm.Employees
{
    public abstract class GetEmployeesInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? IdentityNumber { get; set; }
        public string? EnrolmentNumber { get; set; }
        public EmployeeStatus? Status { get; set; }
        public EmployeeType? Type { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? NoteId { get; set; }

        public GetEmployeesInputBase()
        {

        }
    }
}