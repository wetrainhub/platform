using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace WTH.Platform.Web.Public.Pages;

public class IndexModel : PlatformPublicPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
