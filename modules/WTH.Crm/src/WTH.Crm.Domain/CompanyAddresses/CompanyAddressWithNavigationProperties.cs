using Wth.Crm.Addresses;

using System;
using System.Collections.Generic;

namespace Wth.Crm.CompanyAddresses
{
    public abstract class CompanyAddressWithNavigationPropertiesBase
    {
        public CompanyAddress CompanyAddress { get; set; } = null!;

        public Address Address { get; set; } = null!;
        

        
    }
}