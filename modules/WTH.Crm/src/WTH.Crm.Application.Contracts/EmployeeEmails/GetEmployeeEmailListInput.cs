using Volo.Abp.Application.Dtos;
using System;

namespace Wth.Crm.EmployeeEmails
{
    public class GetEmployeeEmailListInput : PagedAndSortedResultRequestDto
    {
        public Guid EmployeeId { get; set; }
    }
}