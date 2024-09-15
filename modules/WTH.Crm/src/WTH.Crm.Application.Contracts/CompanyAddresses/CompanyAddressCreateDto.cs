using Wth.Crm.Companies;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Wth.Crm.CompanyAddresses
{
    public abstract class CompanyAddressCreateDtoBase
    {
        public Guid CompanyId { get; set; }
        public CompanyAddressType Type { get; set; } = ((CompanyAddressType[])Enum.GetValues(typeof(CompanyAddressType)))[0];
        public Guid AddressId { get; set; }
    }
}