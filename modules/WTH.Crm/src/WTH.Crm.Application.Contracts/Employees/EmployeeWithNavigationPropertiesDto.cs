using Wth.Crm.Companies;
using Wth.Crm.Employees;
using Wth.Crm.Notes;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace Wth.Crm.Employees
{
    public abstract class EmployeeWithNavigationPropertiesDtoBase
    {
        public EmployeeDto Employee { get; set; } = null!;

        public CompanyDto Company { get; set; } = null!;
        public EmployeeDto Employee1 { get; set; } = null!;
        public List<NoteDto> Notes { get; set; } = new List<NoteDto>();

    }
}