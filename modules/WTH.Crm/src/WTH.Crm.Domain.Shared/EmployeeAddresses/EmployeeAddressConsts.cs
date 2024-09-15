namespace Wth.Crm.EmployeeAddresses
{
    public static class EmployeeAddressConsts
    {
        private const string DefaultSorting = "{0}Type asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "EmployeeAddress." : string.Empty);
        }

    }
}