using Wth.Crm.Employees;
using Volo.Abp.Application.Dtos;
using System;

namespace Wth.Crm.EmployeeEmails
{
    public abstract class GetEmployeeEmailsInputBase : PagedAndSortedResultRequestDto
    {
        public Guid? EmployeeId { get; set; }

        public string? FilterText { get; set; }

        public string? Value { get; set; }
        public EmployeeEmailType? Type { get; set; }

        public GetEmployeeEmailsInputBase()
        {

        }
    }
}