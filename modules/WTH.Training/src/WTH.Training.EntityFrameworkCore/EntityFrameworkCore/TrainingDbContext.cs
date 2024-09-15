using WTH.Training.Awards;
using Volo.Abp.EntityFrameworkCore.Modeling;
using WTH.Training.AwardTypes;
using WTH.Training.AwardingOrganisations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace WTH.Training.EntityFrameworkCore;

[ConnectionStringName(TrainingDbProperties.ConnectionStringName)]
public class TrainingDbContext : AbpDbContext<TrainingDbContext>, ITrainingDbContext
{
    public DbSet<Award> Awards { get; set; } = null!;
    public DbSet<AwardType> AwardTypes { get; set; } = null!;
    public DbSet<AwardingOrganisation> AwardingOrganisations { get; set; } = null!;
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public TrainingDbContext(DbContextOptions<TrainingDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureTraining();
    }
}