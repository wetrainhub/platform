using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Wth.Crm.EmployeeEmails;

namespace Wth.Crm.EmployeeEmails
{
    [RemoteService(Name = "Crm")]
    [Area("crm")]
    [ControllerName("EmployeeEmail")]
    [Route("api/crm/employee-emails")]
    public class EmployeeEmailController : EmployeeEmailControllerBase, IEmployeeEmailsAppService
    {
        public EmployeeEmailController(IEmployeeEmailsAppService employeeEmailsAppService) : base(employeeEmailsAppService)
        {
        }
    }
}