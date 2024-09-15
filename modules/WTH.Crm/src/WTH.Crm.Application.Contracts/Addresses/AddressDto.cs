using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Wth.Crm.Addresses
{
    public class AddressDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Line1 { get; set; } = null!;
        public string? Line2 { get; set; }
        public string? Line3 { get; set; }
        public string City { get; set; } = null!;
        public string County { get; set; } = null!;
        public string Postcode { get; set; } = null!;
        public string? What3Words { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}