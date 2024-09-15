namespace Wth.Crm.Notes
{
    public static class NoteConsts
    {
        private const string DefaultSorting = "{0}Content asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Note." : string.Empty);
        }

    }
}