using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WTH.Training.AwardTypes
{
    public abstract class AwardTypeCreateDtoBase
    {
        [Required]
        public string Name { get; set; } = null!;
        public bool HasReferenceNumber { get; set; }
        public bool HasExpiryDate { get; set; }
    }
}