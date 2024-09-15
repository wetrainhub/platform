using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace Wth.Crm.Notes
{
    public abstract class NoteBase : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Content { get; set; }

        protected NoteBase()
        {

        }

        public NoteBase(Guid id, string content)
        {

            Id = id;
            Check.NotNull(content, nameof(content));
            Content = content;
        }

    }
}