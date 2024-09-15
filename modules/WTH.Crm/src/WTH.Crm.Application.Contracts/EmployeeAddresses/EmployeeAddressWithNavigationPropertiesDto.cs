using Wth.Crm.Addresses;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace Wth.Crm.EmployeeAddresses
{
    public abstract class EmployeeAddressWithNavigationPropertiesDtoBase
    {
        public EmployeeAddressDto EmployeeAddress { get; set; } = null!;

        public AddressDto Address { get; set; } = null!;

    }
}