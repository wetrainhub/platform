namespace Wth.Crm.CompanyEmails
{
    public static class CompanyEmailConsts
    {
        private const string DefaultSorting = "{0}Value asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CompanyEmail." : string.Empty);
        }

    }
}