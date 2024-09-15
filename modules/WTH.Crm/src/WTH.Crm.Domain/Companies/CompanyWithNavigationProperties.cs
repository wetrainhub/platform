using Wth.Crm.Notes;

using System;
using System.Collections.Generic;

namespace Wth.Crm.Companies
{
    public abstract class CompanyWithNavigationPropertiesBase
    {
        public Company Company { get; set; } = null!;

        

        public List<Note> Notes { get; set; } = null!;
        
    }
}