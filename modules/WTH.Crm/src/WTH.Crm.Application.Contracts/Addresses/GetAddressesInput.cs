using Volo.Abp.Application.Dtos;
using System;

namespace Wth.Crm.Addresses
{
    public class GetAddressesInput : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? Line3 { get; set; }
        public string? City { get; set; }
        public string? County { get; set; }
        public string? Postcode { get; set; }

        public GetAddressesInput()
        {

        }
    }
}