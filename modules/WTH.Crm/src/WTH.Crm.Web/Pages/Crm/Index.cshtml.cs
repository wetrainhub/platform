using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.Application.Dtos;
using Wth.Crm.Companies;

namespace Wth.Crm.Web.Pages.Crm;

public class IndexModel(ICompaniesAppService companiesAppService) : PageModel
{
    [BindProperty(SupportsGet = true)] public GetCompaniesInput Filter { get; set; }

    public async Task OnGetAsync()
    {
        SearchResults = await companiesAppService.GetListAsync(Filter);
    }

    public PagedResultDto<CompanyWithNavigationPropertiesDto> SearchResults { get; set; }
}