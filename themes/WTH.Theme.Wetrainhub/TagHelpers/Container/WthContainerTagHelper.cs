using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace WTH.Theme.Wetrainhub.TagHelpers.Container;

public class WthContainerTagHelper(WthContainerTagHelperService tagHelperService)
    : AbpTagHelper<WthContainerTagHelper, WthContainerTagHelperService>(tagHelperService);