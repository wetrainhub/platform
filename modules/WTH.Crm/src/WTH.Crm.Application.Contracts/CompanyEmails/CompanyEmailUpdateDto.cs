using Wth.Crm.Companies;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Wth.Crm.CompanyEmails
{
    public abstract class CompanyEmailUpdateDtoBase
    {
        public Guid CompanyId { get; set; }
        [Required]
        [EmailAddress]
        public string Value { get; set; } = null!;
        public CompanyEmailType Type { get; set; }

    }
}