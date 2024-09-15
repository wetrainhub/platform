using WTH.Training.Awards;
using Volo.Abp.EntityFrameworkCore.Modeling;
using WTH.Training.AwardTypes;
using WTH.Training.AwardingOrganisations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace WTH.Training.EntityFrameworkCore;

[ConnectionStringName(TrainingDbProperties.ConnectionStringName)]
public interface ITrainingDbContext : IEfCoreDbContext
{
    DbSet<Award> Awards { get; set; }
    DbSet<AwardType> AwardTypes { get; set; }
    DbSet<AwardingOrganisation> AwardingOrganisations { get; set; }
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}