using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Wth.Crm.Companies
{
    public abstract class CompanyCreateDtoBase
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? TaxReference { get; set; }
        public List<Guid> NoteIds { get; set; }
    }
}