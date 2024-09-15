namespace WTH.Training.AwardTypes
{
    public static class AwardTypeConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "AwardType." : string.Empty);
        }

    }
}