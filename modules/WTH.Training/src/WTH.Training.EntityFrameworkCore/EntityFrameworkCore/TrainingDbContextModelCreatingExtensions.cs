using WTH.Training.Awards;
using WTH.Training.AwardTypes;
using Volo.Abp.EntityFrameworkCore.Modeling;
using WTH.Training.AwardingOrganisations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace WTH.Training.EntityFrameworkCore;

public static class TrainingDbContextModelCreatingExtensions
{
    public static void ConfigureTraining(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(TrainingDbProperties.DbTablePrefix + "Questions", TrainingDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */

        builder.Entity<AwardType>(b =>
                {
                    b.ToTable(TrainingDbProperties.DbTablePrefix + "AwardTypes", TrainingDbProperties.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.TenantId).HasColumnName(nameof(AwardType.TenantId));
                    b.Property(x => x.Name).HasColumnName(nameof(AwardType.Name)).IsRequired();
                    b.Property(x => x.HasReferenceNumber).HasColumnName(nameof(AwardType.HasReferenceNumber));
                    b.Property(x => x.HasExpiryDate).HasColumnName(nameof(AwardType.HasExpiryDate));
                });

        builder.Entity<AwardingOrganisation>(b =>
                {
                    b.ToTable(TrainingDbProperties.DbTablePrefix + "AwardingOrganisations", TrainingDbProperties.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.TenantId).HasColumnName(nameof(AwardingOrganisation.TenantId));
                    b.Property(x => x.Name).HasColumnName(nameof(AwardingOrganisation.Name)).IsRequired();
                    b.Property(x => x.Email).HasColumnName(nameof(AwardingOrganisation.Email));
                    b.Property(x => x.Telephone).HasColumnName(nameof(AwardingOrganisation.Telephone));
                });
        builder.Entity<Award>(b =>
                {
                    b.ToTable(TrainingDbProperties.DbTablePrefix + "Awards", TrainingDbProperties.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.TenantId).HasColumnName(nameof(Award.TenantId));
                    b.Property(x => x.Name).HasColumnName(nameof(Award.Name)).IsRequired();
                    b.Property(x => x.Description).HasColumnName(nameof(Award.Description));
                    b.Property(x => x.Code).HasColumnName(nameof(Award.Code)).IsRequired();
                    b.HasOne<AwardType>().WithMany().IsRequired().HasForeignKey(x => x.AwardTypeId).OnDelete(DeleteBehavior.NoAction);
                    b.HasOne<AwardingOrganisation>().WithMany().IsRequired().HasForeignKey(x => x.AwardingOrganisationId).OnDelete(DeleteBehavior.NoAction);
                });
    }
}