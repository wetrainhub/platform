using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using WTH.Training.EntityFrameworkCore;

namespace WTH.Training.Awards
{
    public class EfCoreAwardRepository : EfCoreAwardRepositoryBase, IAwardRepository
    {
        public EfCoreAwardRepository(IDbContextProvider<TrainingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}