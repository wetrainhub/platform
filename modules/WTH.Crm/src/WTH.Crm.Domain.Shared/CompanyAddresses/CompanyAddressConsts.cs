namespace Wth.Crm.CompanyAddresses
{
    public static class CompanyAddressConsts
    {
        private const string DefaultSorting = "{0}Type asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CompanyAddress." : string.Empty);
        }

    }
}