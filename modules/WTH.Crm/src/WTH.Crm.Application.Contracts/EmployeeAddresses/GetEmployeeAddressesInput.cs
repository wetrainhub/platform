using Wth.Crm.Employees;
using Volo.Abp.Application.Dtos;
using System;

namespace Wth.Crm.EmployeeAddresses
{
    public abstract class GetEmployeeAddressesInputBase : PagedAndSortedResultRequestDto
    {
        public Guid? EmployeeId { get; set; }

        public string? FilterText { get; set; }

        public EmployeeAddressType? Type { get; set; }
        public Guid? AddressId { get; set; }

        public GetEmployeeAddressesInputBase()
        {

        }
    }
}