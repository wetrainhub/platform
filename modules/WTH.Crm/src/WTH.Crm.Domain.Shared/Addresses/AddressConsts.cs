namespace Wth.Crm.Addresses
{
    public static class AddressConsts
    {
        private const string DefaultSorting = "{0}Line1 asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Address." : string.Empty);
        }

    }
}