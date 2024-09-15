namespace Wth.Crm.EmployeeEmails
{
    public static class EmployeeEmailConsts
    {
        private const string DefaultSorting = "{0}Value asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "EmployeeEmail." : string.Empty);
        }

    }
}