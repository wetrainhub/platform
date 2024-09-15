using Wth.Crm.Notes;
using Wth.Crm.CompanyTelephones;
using Wth.Crm.CompanyEmails;
using Wth.Crm.CompanyAddresses;
using Wth.Crm.Employees;
using Volo.Abp.AutoMapper;
using Wth.Crm.EmployeeAddresses;
using Wth.Crm.EmployeeTelephones;
using Wth.Crm.EmployeeEmails;
using Wth.Crm.Addresses;
using System;
using Wth.Crm.Shared;
using Wth.Crm.Companies;
using AutoMapper;

namespace Wth.Crm;

public class CrmApplicationAutoMapperProfile : Profile
{
    public CrmApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Company, CompanyDto>();

        CreateMap<Address, AddressDto>();

        CreateMap<CompanyWithNavigationProperties, CompanyWithNavigationPropertiesDto>();
        CreateMap<Address, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Line1));

        CreateMap<EmployeeEmail, EmployeeEmailDto>();

        CreateMap<EmployeeTelephone, EmployeeTelephoneDto>();

        CreateMap<EmployeeAddress, EmployeeAddressDto>();
        CreateMap<EmployeeAddressWithNavigationProperties, EmployeeAddressWithNavigationPropertiesDto>();

        CreateMap<Employee, EmployeeDto>().Ignore(x => x.EmployeeEmails).Ignore(x => x.EmployeeTelephones).Ignore(x => x.EmployeeAddresses);

        CreateMap<CompanyAddress, CompanyAddressDto>();
        CreateMap<CompanyAddressWithNavigationProperties, CompanyAddressWithNavigationPropertiesDto>();

        CreateMap<CompanyEmail, CompanyEmailDto>();

        CreateMap<CompanyTelephone, CompanyTelephoneDto>();

        CreateMap<EmployeeWithNavigationProperties, EmployeeWithNavigationPropertiesDto>();
        CreateMap<Company, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Employee, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.FirstName));

        CreateMap<Note, NoteDto>();

        CreateMap<Company, CompanyDto>().Ignore(x => x.CompanyAddresses).Ignore(x => x.CompanyEmails).Ignore(x => x.CompanyTelephones);
        CreateMap<Note, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Content));
    }
}