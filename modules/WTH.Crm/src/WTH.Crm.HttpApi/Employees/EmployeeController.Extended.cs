using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Wth.Crm.Employees;

namespace Wth.Crm.Employees
{
    [RemoteService(Name = "Crm")]
    [Area("crm")]
    [ControllerName("Employee")]
    [Route("api/crm/employees")]
    public class EmployeeController : EmployeeControllerBase, IEmployeesAppService
    {
        public EmployeeController(IEmployeesAppService employeesAppService) : base(employeesAppService)
        {
        }
    }
}