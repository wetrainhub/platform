using Wth.Crm.Shared;
using Wth.Crm.Addresses;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Wth.Crm.Permissions;
using Wth.Crm.CompanyAddresses;

namespace Wth.Crm.CompanyAddresses
{
    public class CompanyAddressesAppService : CompanyAddressesAppServiceBase, ICompanyAddressesAppService
    {
        //<suite-custom-code-autogenerated>
        public CompanyAddressesAppService(ICompanyAddressRepository companyAddressRepository, CompanyAddressManager companyAddressManager, IRepository<Wth.Crm.Addresses.Address, Guid> addressRepository)
            : base(companyAddressRepository, companyAddressManager, addressRepository)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}