using Volo.Abp.Reflection;

namespace WTH.Training.Permissions;

public class TrainingPermissions
{
    public const string GroupName = "Training";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(TrainingPermissions));
    }

    public static class AwardingOrganisations
    {
        public const string Default = GroupName + ".AwardingOrganisations";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Awards
    {
        public const string Default = GroupName + ".Awards";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class AwardTypes
    {
        public const string Default = GroupName + ".AwardTypes";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}