using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Wth.Crm.EntityFrameworkCore;

namespace Wth.Crm.CompanyEmails
{
    public class EfCoreCompanyEmailRepository : EfCoreCompanyEmailRepositoryBase, ICompanyEmailRepository
    {
        public EfCoreCompanyEmailRepository(IDbContextProvider<CrmDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}