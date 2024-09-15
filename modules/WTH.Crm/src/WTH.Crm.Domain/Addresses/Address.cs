using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

using Volo.Abp;

namespace Wth.Crm.Addresses
{
    public class Address : FullAuditedEntity<Guid>, IMultiTenant, IHasConcurrencyStamp
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Line1 { get; set; }

        [CanBeNull]
        public virtual string? Line2 { get; set; }

        [CanBeNull]
        public virtual string? Line3 { get; set; }

        [NotNull]
        public virtual string City { get; set; }

        [NotNull]
        public virtual string County { get; set; }

        [NotNull]
        public virtual string Postcode { get; set; }

        [CanBeNull]
        public virtual string? What3Words { get; set; }

        public virtual decimal? Latitude { get; set; }

        public virtual decimal? Longitude { get; set; }

        public string ConcurrencyStamp { get; set; }

        protected Address()
        {

        }

        public Address(Guid id, string line1, string city, string county, string postcode, string? line2 = null, string? line3 = null)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            Id = id;
            Check.NotNull(line1, nameof(line1));
            Check.NotNull(city, nameof(city));
            Check.NotNull(county, nameof(county));
            Check.NotNull(postcode, nameof(postcode));
            Line1 = line1;
            City = city;
            County = county;
            Postcode = postcode;
            Line2 = line2;
            Line3 = line3;
        }

    }
}