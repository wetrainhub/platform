using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Wth.Crm.Notes
{
    public abstract class NoteCreateDtoBase
    {
        [Required]
        public string Content { get; set; } = null!;
    }
}