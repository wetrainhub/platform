using WTH.Training.Awards;
using WTH.Training.AwardTypes;
using WTH.Training.AwardingOrganisations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace WTH.Training.EntityFrameworkCore;

[DependsOn(
    typeof(TrainingDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class TrainingEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<TrainingDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<AwardingOrganisation, AwardingOrganisations.EfCoreAwardingOrganisationRepository>();

            options.AddRepository<AwardType, AwardTypes.EfCoreAwardTypeRepository>();

            options.AddRepository<Award, Awards.EfCoreAwardRepository>();

        });
    }
}