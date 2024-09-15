using Volo.Abp.Application.Dtos;
using System;

namespace WTH.Training.AwardTypes
{
    public abstract class GetAwardTypesInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public bool? HasReferenceNumber { get; set; }
        public bool? HasExpiryDate { get; set; }

        public GetAwardTypesInputBase()
        {

        }
    }
}