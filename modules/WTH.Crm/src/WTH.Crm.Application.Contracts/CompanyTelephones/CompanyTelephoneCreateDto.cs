using Wth.Crm.Companies;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Wth.Crm.CompanyTelephones
{
    public abstract class CompanyTelephoneCreateDtoBase
    {
        public Guid CompanyId { get; set; }
        [Required]
        public string Value { get; set; } = null!;
        public CompanyTelephoneType Type { get; set; } = ((CompanyTelephoneType[])Enum.GetValues(typeof(CompanyTelephoneType)))[0];
    }
}