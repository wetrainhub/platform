namespace WTH.Training.AwardingOrganisations
{
    public static class AwardingOrganisationConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "AwardingOrganisation." : string.Empty);
        }

    }
}