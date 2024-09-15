using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Wth.Crm.Addresses
{
    public interface IAddressRepository : IRepository<Address, Guid>
    {
        Task<List<Address>> GetListAsync(
            string? filterText = null,
            string? line1 = null,
            string? line2 = null,
            string? line3 = null,
            string? city = null,
            string? county = null,
            string? postcode = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? line1 = null,
            string? line2 = null,
            string? line3 = null,
            string? city = null,
            string? county = null,
            string? postcode = null,
            CancellationToken cancellationToken = default);
    }
}