namespace Wth.Crm.EmployeeTelephones
{
    public static class EmployeeTelephoneConsts
    {
        private const string DefaultSorting = "{0}Value asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "EmployeeTelephone." : string.Empty);
        }

    }
}