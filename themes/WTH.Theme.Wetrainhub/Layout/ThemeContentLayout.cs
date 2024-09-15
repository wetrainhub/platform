using System;
using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Layout;

namespace WTH.Theme.Wetrainhub.Layout;

public class ThemeContentLayout 
{
    public string? MenuTabName { get; private set; }
    public string? MenuItemName { get; private set; }
    
    public void SetMenu(string tabName, string menuItemName)
    {
        MenuTabName = tabName;
        MenuItemName = menuItemName;
    }
    
    
    /// <summary>
    /// Page title
    /// </summary>
    public string? Title { get; private set; }
    /// <summary>
    /// Page description
    /// </summary>
    public string? Description { get; private set; }

    public void SetTitleWithDescription(string title, string description)
    {
        Title = title;
        Description = description;
        BreadCrumb.Items.Clear();
    }
    
    public BreadCrumb BreadCrumb { get; } = new();
    
    public void SetTitleWithBreadCrumb(string title,List<BreadCrumbItem> breadCrumbItems)
    {
        Title = title;
        Description = null;
        BreadCrumb.Items.AddRange(breadCrumbItems);
    }
    

    
    public virtual bool ShouldShowBreadCrumb()
    {
        if (BreadCrumb.Items.Count != 0)
        {
            return true;
        }

        return BreadCrumb.ShowCurrent && !Title.IsNullOrEmpty();
    }
}