using Wth.Crm.Addresses;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace Wth.Crm.CompanyAddresses
{
    public abstract class CompanyAddressWithNavigationPropertiesDtoBase
    {
        public CompanyAddressDto CompanyAddress { get; set; } = null!;

        public AddressDto Address { get; set; } = null!;

    }
}