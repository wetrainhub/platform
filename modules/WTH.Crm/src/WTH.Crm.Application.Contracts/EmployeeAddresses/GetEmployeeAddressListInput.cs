using Volo.Abp.Application.Dtos;
using System;

namespace Wth.Crm.EmployeeAddresses
{
    public class GetEmployeeAddressListInput : PagedAndSortedResultRequestDto
    {
        public Guid EmployeeId { get; set; }
    }
}