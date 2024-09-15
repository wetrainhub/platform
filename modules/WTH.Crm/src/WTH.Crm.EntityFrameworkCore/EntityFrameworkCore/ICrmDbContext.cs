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
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Wth.Crm.EntityFrameworkCore;

[ConnectionStringName(CrmDbProperties.ConnectionStringName)]
public interface ICrmDbContext : IEfCoreDbContext
{
    DbSet<Note> Notes { get; set; }
    DbSet<CompanyTelephone> CompanyTelephones { get; set; }
    DbSet<CompanyEmail> CompanyEmails { get; set; }
    DbSet<CompanyAddress> CompanyAddresses { get; set; }
    DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
    DbSet<EmployeeTelephone> EmployeeTelephones { get; set; }
    DbSet<EmployeeEmail> EmployeeEmails { get; set; }
    DbSet<Employee> Employees { get; set; }
    DbSet<Address> Addresses { get; set; }
    DbSet<Company> Companies { get; set; }
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}