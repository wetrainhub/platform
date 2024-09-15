using Volo.Abp.Application.Dtos;
using System;

namespace Wth.Crm.EmployeeTelephones
{
    public class GetEmployeeTelephoneListInput : PagedAndSortedResultRequestDto
    {
        public Guid EmployeeId { get; set; }
    }
}