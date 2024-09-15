using Volo.Abp.EntityFrameworkCore;
using WTH.Training.EntityFrameworkCore;

namespace WTH.Training.AwardTypes
{
    public class EfCoreAwardTypeRepository : EfCoreAwardTypeRepositoryBase, IAwardTypeRepository
    {
        public EfCoreAwardTypeRepository(IDbContextProvider<TrainingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}