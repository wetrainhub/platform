using Wth.Crm.Companies;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Wth.Crm.CompanyAddresses
{
    public abstract class CompanyAddressUpdateDtoBase
    {
        public Guid CompanyId { get; set; }
        public CompanyAddressType Type { get; set; }
        public Guid AddressId { get; set; }

    }
}