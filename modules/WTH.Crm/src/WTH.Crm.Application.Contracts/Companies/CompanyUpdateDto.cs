using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Wth.Crm.Companies
{
    public abstract class CompanyUpdateDtoBase : IHasConcurrencyStamp
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? TaxReference { get; set; }
        public List<Guid> NoteIds { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}