namespace WTH.Training.Awards
{
    public static class AwardConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Award." : string.Empty);
        }

    }
}