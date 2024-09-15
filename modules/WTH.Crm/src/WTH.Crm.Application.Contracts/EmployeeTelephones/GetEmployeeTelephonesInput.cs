using Wth.Crm.Employees;
using Volo.Abp.Application.Dtos;
using System;

namespace Wth.Crm.EmployeeTelephones
{
    public abstract class GetEmployeeTelephonesInputBase : PagedAndSortedResultRequestDto
    {
        public Guid? EmployeeId { get; set; }

        public string? FilterText { get; set; }

        public string? Value { get; set; }
        public EmployeeTelephoneType? Type { get; set; }

        public GetEmployeeTelephonesInputBase()
        {

        }
    }
}