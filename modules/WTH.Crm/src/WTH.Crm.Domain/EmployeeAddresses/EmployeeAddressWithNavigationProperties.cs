using Wth.Crm.Addresses;

using System;
using System.Collections.Generic;

namespace Wth.Crm.EmployeeAddresses
{
    public abstract class EmployeeAddressWithNavigationPropertiesBase
    {
        public EmployeeAddress EmployeeAddress { get; set; } = null!;

        public Address Address { get; set; } = null!;
        

        
    }
}