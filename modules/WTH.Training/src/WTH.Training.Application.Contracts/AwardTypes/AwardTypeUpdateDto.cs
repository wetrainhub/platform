using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace WTH.Training.AwardTypes
{
    public abstract class AwardTypeUpdateDtoBase : IHasConcurrencyStamp
    {
        [Required]
        public string Name { get; set; } = null!;
        public bool HasReferenceNumber { get; set; }
        public bool HasExpiryDate { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}