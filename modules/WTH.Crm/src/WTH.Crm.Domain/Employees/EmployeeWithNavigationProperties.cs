using Wth.Crm.Companies;
using Wth.Crm.Employees;
using Wth.Crm.Notes;

using System;
using System.Collections.Generic;

namespace Wth.Crm.Employees
{
    public abstract class EmployeeWithNavigationPropertiesBase
    {
        public Employee Employee { get; set; } = null!;

        public Company Company { get; set; } = null!;
        public Employee Employee1 { get; set; } = null!;
        

        public List<Note> Notes { get; set; } = null!;
        
    }
}