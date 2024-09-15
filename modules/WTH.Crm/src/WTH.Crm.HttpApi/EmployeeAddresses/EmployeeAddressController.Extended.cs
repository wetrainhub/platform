using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Wth.Crm.EmployeeAddresses;

namespace Wth.Crm.EmployeeAddresses
{
    [RemoteService(Name = "Crm")]
    [Area("crm")]
    [ControllerName("EmployeeAddress")]
    [Route("api/crm/employee-addresses")]
    public class EmployeeAddressController : EmployeeAddressControllerBase, IEmployeeAddressesAppService
    {
        public EmployeeAddressController(IEmployeeAddressesAppService employeeAddressesAppService) : base(employeeAddressesAppService)
        {
        }
    }
}