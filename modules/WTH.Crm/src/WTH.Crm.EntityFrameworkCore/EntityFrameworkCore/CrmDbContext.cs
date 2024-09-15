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
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Wth.Crm.EntityFrameworkCore;

[ConnectionStringName(CrmDbProperties.ConnectionStringName)]
public class CrmDbContext : AbpDbContext<CrmDbContext>, ICrmDbContext
{
    public DbSet<Note> Notes { get; set; } = null!;
    public DbSet<CompanyTelephone> CompanyTelephones { get; set; } = null!;
    public DbSet<CompanyEmail> CompanyEmails { get; set; } = null!;
    public DbSet<CompanyAddress> CompanyAddresses { get; set; } = null!;
    public DbSet<EmployeeAddress> EmployeeAddresses { get; set; } = null!;
    public DbSet<EmployeeTelephone> EmployeeTelephones { get; set; } = null!;
    public DbSet<EmployeeEmail> EmployeeEmails { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public CrmDbContext(DbContextOptions<CrmDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureCrm();
    }
}