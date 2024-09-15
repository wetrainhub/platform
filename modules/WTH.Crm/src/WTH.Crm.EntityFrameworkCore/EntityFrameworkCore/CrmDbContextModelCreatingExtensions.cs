using Wth.Crm.Notes;
using Wth.Crm.CompanyTelephones;
using Wth.Crm.CompanyEmails;
using Wth.Crm.CompanyAddresses;
using Wth.Crm.EmployeeAddresses;
using Wth.Crm.EmployeeTelephones;
using Wth.Crm.EmployeeEmails;
using Wth.Crm.Employees;
using Wth.Crm.Addresses;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Wth.Crm.Companies;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Wth.Crm.EntityFrameworkCore;

public static class CrmDbContextModelCreatingExtensions
{
    public static void ConfigureCrm(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(CrmDbProperties.DbTablePrefix + "Questions", CrmDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */

        builder.Entity<Address>(b =>
                {
                    b.ToTable(CrmDbProperties.DbTablePrefix + "Addresses", CrmDbProperties.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.TenantId).HasColumnName(nameof(Address.TenantId));
                    b.Property(x => x.Line1).HasColumnName(nameof(Address.Line1)).IsRequired();
                    b.Property(x => x.Line2).HasColumnName(nameof(Address.Line2));
                    b.Property(x => x.Line3).HasColumnName(nameof(Address.Line3));
                    b.Property(x => x.City).HasColumnName(nameof(Address.City)).IsRequired();
                    b.Property(x => x.County).HasColumnName(nameof(Address.County)).IsRequired();
                    b.Property(x => x.Postcode).HasColumnName(nameof(Address.Postcode)).IsRequired();
                    b.Property(x => x.What3Words).HasColumnName(nameof(Address.What3Words));
                    b.Property(x => x.Latitude).HasColumnName(nameof(Address.Latitude));
                    b.Property(x => x.Longitude).HasColumnName(nameof(Address.Longitude));
                });

        builder.Entity<EmployeeEmail>(b =>
                {
                    b.ToTable(CrmDbProperties.DbTablePrefix + "EmployeeEmails", CrmDbProperties.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.TenantId).HasColumnName(nameof(EmployeeEmail.TenantId));
                    b.Property(x => x.Value).HasColumnName(nameof(EmployeeEmail.Value)).IsRequired();
                    b.Property(x => x.Type).HasColumnName(nameof(EmployeeEmail.Type));
                    b.HasOne<Employee>().WithMany(x => x.EmployeeEmails).HasForeignKey(x => x.EmployeeId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                });

        builder.Entity<EmployeeTelephone>(b =>
                {
                    b.ToTable(CrmDbProperties.DbTablePrefix + "EmployeeTelephones", CrmDbProperties.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.TenantId).HasColumnName(nameof(EmployeeTelephone.TenantId));
                    b.Property(x => x.Value).HasColumnName(nameof(EmployeeTelephone.Value)).IsRequired();
                    b.Property(x => x.Type).HasColumnName(nameof(EmployeeTelephone.Type));
                    b.HasOne<Employee>().WithMany(x => x.EmployeeTelephones).HasForeignKey(x => x.EmployeeId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                });

        builder.Entity<CompanyAddress>(b =>
                {
                    b.ToTable(CrmDbProperties.DbTablePrefix + "CompanyAddresses", CrmDbProperties.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.TenantId).HasColumnName(nameof(CompanyAddress.TenantId));
                    b.Property(x => x.Type).HasColumnName(nameof(CompanyAddress.Type));
                    b.HasOne<Address>().WithMany().IsRequired().HasForeignKey(x => x.AddressId).OnDelete(DeleteBehavior.NoAction);
                    b.HasOne<Company>().WithMany(x => x.CompanyAddresses).HasForeignKey(x => x.CompanyId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                });

        builder.Entity<CompanyEmail>(b =>
                {
                    b.ToTable(CrmDbProperties.DbTablePrefix + "CompanyEmails", CrmDbProperties.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.TenantId).HasColumnName(nameof(CompanyEmail.TenantId));
                    b.Property(x => x.Value).HasColumnName(nameof(CompanyEmail.Value)).IsRequired();
                    b.Property(x => x.Type).HasColumnName(nameof(CompanyEmail.Type));
                    b.HasOne<Company>().WithMany(x => x.CompanyEmails).HasForeignKey(x => x.CompanyId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                });

        builder.Entity<CompanyTelephone>(b =>
                {
                    b.ToTable(CrmDbProperties.DbTablePrefix + "CompanyTelephones", CrmDbProperties.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.TenantId).HasColumnName(nameof(CompanyTelephone.TenantId));
                    b.Property(x => x.Value).HasColumnName(nameof(CompanyTelephone.Value)).IsRequired();
                    b.Property(x => x.Type).HasColumnName(nameof(CompanyTelephone.Type));
                    b.HasOne<Company>().WithMany(x => x.CompanyTelephones).HasForeignKey(x => x.CompanyId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                });

        builder.Entity<Note>(b =>
                {
                    b.ToTable(CrmDbProperties.DbTablePrefix + "Notes", CrmDbProperties.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.TenantId).HasColumnName(nameof(Note.TenantId));
                    b.Property(x => x.Content).HasColumnName(nameof(Note.Content)).IsRequired();
                });
        builder.Entity<Company>(b =>
                {
                    b.ToTable(CrmDbProperties.DbTablePrefix + "Companies", CrmDbProperties.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.TenantId).HasColumnName(nameof(Company.TenantId));
                    b.Property(x => x.Name).HasColumnName(nameof(Company.Name)).IsRequired();
                    b.Property(x => x.TaxReference).HasColumnName(nameof(Company.TaxReference));
                    b.HasMany(x => x.CompanyAddresses).WithOne().HasForeignKey(x => x.CompanyId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                    b.HasMany(x => x.CompanyEmails).WithOne().HasForeignKey(x => x.CompanyId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                    b.HasMany(x => x.CompanyTelephones).WithOne().HasForeignKey(x => x.CompanyId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                    b.HasMany(x => x.Notes).WithOne().HasForeignKey(x => x.CompanyId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                });

        builder.Entity<CompanyNote>(b =>
    {
        b.ToTable(CrmDbProperties.DbTablePrefix + "CompanyNote", CrmDbProperties.DbSchema);
        b.ConfigureByConvention();

        b.HasKey(
            x => new { x.CompanyId, x.NoteId }
        );

        b.HasOne<Company>().WithMany(x => x.Notes).HasForeignKey(x => x.CompanyId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        b.HasOne<Note>().WithMany().HasForeignKey(x => x.NoteId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        b.HasIndex(
                x => new { x.CompanyId, x.NoteId }
        );
    });
        builder.Entity<Employee>(b =>
                {
                    b.ToTable(CrmDbProperties.DbTablePrefix + "Employees", CrmDbProperties.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.TenantId).HasColumnName(nameof(Employee.TenantId));
                    b.Property(x => x.FirstName).HasColumnName(nameof(Employee.FirstName)).IsRequired();
                    b.Property(x => x.MiddleName).HasColumnName(nameof(Employee.MiddleName));
                    b.Property(x => x.LastName).HasColumnName(nameof(Employee.LastName)).IsRequired();
                    b.Property(x => x.IdentityNumber).HasColumnName(nameof(Employee.IdentityNumber));
                    b.Property(x => x.EnrolmentNumber).HasColumnName(nameof(Employee.EnrolmentNumber));
                    b.Property(x => x.DateOfBirth).HasColumnName(nameof(Employee.DateOfBirth));
                    b.Property(x => x.Status).HasColumnName(nameof(Employee.Status));
                    b.Property(x => x.Type).HasColumnName(nameof(Employee.Type));
                    b.HasOne<Company>().WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.SetNull);
                    b.HasOne<Employee>().WithMany().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.SetNull);
                    b.HasMany(x => x.EmployeeEmails).WithOne().HasForeignKey(x => x.EmployeeId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                    b.HasMany(x => x.EmployeeTelephones).WithOne().HasForeignKey(x => x.EmployeeId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                    b.HasMany(x => x.EmployeeAddresses).WithOne().HasForeignKey(x => x.EmployeeId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                    b.HasMany(x => x.Notes).WithOne().HasForeignKey(x => x.EmployeeId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                });

        builder.Entity<EmployeeNote>(b =>
    {
        b.ToTable(CrmDbProperties.DbTablePrefix + "EmployeeNote", CrmDbProperties.DbSchema);
        b.ConfigureByConvention();

        b.HasKey(
            x => new { x.EmployeeId, x.NoteId }
        );

        b.HasOne<Employee>().WithMany(x => x.Notes).HasForeignKey(x => x.EmployeeId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        b.HasOne<Note>().WithMany().HasForeignKey(x => x.NoteId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        b.HasIndex(
                x => new { x.EmployeeId, x.NoteId }
        );
    });
    }
}