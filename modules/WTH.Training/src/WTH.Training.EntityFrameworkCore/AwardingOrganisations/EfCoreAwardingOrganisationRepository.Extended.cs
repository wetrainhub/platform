using Volo.Abp.EntityFrameworkCore;
using WTH.Training.EntityFrameworkCore;

namespace WTH.Training.AwardingOrganisations
{
    public class EfCoreAwardingOrganisationRepository : EfCoreAwardingOrganisationRepositoryBase, IAwardingOrganisationRepository
    {
        public EfCoreAwardingOrganisationRepository(IDbContextProvider<TrainingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}