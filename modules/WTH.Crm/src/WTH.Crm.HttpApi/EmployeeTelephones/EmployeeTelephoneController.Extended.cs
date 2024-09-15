using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Wth.Crm.EmployeeTelephones;

namespace Wth.Crm.EmployeeTelephones
{
    [RemoteService(Name = "Crm")]
    [Area("crm")]
    [ControllerName("EmployeeTelephone")]
    [Route("api/crm/employee-telephones")]
    public class EmployeeTelephoneController : EmployeeTelephoneControllerBase, IEmployeeTelephonesAppService
    {
        public EmployeeTelephoneController(IEmployeeTelephonesAppService employeeTelephonesAppService) : base(employeeTelephonesAppService)
        {
        }
    }
}