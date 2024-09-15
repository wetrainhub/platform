using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Wth.Crm.Notes
{
    public abstract class NoteUpdateDtoBase : IHasConcurrencyStamp
    {
        [Required]
        public string Content { get; set; } = null!;

        public string ConcurrencyStamp { get; set; } = null!;
    }
}