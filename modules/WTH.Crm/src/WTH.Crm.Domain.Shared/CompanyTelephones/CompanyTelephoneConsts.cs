namespace Wth.Crm.CompanyTelephones
{
    public static class CompanyTelephoneConsts
    {
        private const string DefaultSorting = "{0}Value asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CompanyTelephone." : string.Empty);
        }

    }
}