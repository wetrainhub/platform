using Wth.Crm.Notes;
using Wth.Crm.CompanyTelephones;
using Wth.Crm.CompanyEmails;
using Wth.Crm.CompanyAddresses;
using Wth.Crm.EmployeeAddresses;
using Wth.Crm.EmployeeTelephones;
using Wth.Crm.EmployeeEmails;
using Wth.Crm.Employees;
using Wth.Crm.Addresses;
using Wth.Crm.Companies;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Wth.Crm.EntityFrameworkCore;

[DependsOn(
    typeof(CrmDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class CrmEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<CrmDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<Company, Companies.EfCoreCompanyRepository>();

            options.AddRepository<Address, Addresses.EfCoreAddressRepository>();

            options.AddRepository<Employee, Employees.EfCoreEmployeeRepository>();

            options.AddRepository<EmployeeEmail, EmployeeEmails.EfCoreEmployeeEmailRepository>();

            options.AddRepository<EmployeeTelephone, EmployeeTelephones.EfCoreEmployeeTelephoneRepository>();

            options.AddRepository<EmployeeAddress, EmployeeAddresses.EfCoreEmployeeAddressRepository>();

            options.AddRepository<CompanyAddress, CompanyAddresses.EfCoreCompanyAddressRepository>();

            options.AddRepository<CompanyEmail, CompanyEmails.EfCoreCompanyEmailRepository>();

            options.AddRepository<CompanyTelephone, CompanyTelephones.EfCoreCompanyTelephoneRepository>();

            options.AddRepository<Note, Notes.EfCoreNoteRepository>();

        });
    }
}