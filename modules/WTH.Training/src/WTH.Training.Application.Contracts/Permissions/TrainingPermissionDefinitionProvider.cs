using WTH.Training.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace WTH.Training.Permissions;

public class TrainingPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TrainingPermissions.GroupName, L("Permission:Training"));

        var awardingOrganisationPermission = myGroup.AddPermission(TrainingPermissions.AwardingOrganisations.Default, L("Permission:AwardingOrganisations"));
        awardingOrganisationPermission.AddChild(TrainingPermissions.AwardingOrganisations.Create, L("Permission:Create"));
        awardingOrganisationPermission.AddChild(TrainingPermissions.AwardingOrganisations.Edit, L("Permission:Edit"));
        awardingOrganisationPermission.AddChild(TrainingPermissions.AwardingOrganisations.Delete, L("Permission:Delete"));

        var awardPermission = myGroup.AddPermission(TrainingPermissions.Awards.Default, L("Permission:Awards"));
        awardPermission.AddChild(TrainingPermissions.Awards.Create, L("Permission:Create"));
        awardPermission.AddChild(TrainingPermissions.Awards.Edit, L("Permission:Edit"));
        awardPermission.AddChild(TrainingPermissions.Awards.Delete, L("Permission:Delete"));

        var awardTypePermission = myGroup.AddPermission(TrainingPermissions.AwardTypes.Default, L("Permission:AwardTypes"));
        awardTypePermission.AddChild(TrainingPermissions.AwardTypes.Create, L("Permission:Create"));
        awardTypePermission.AddChild(TrainingPermissions.AwardTypes.Edit, L("Permission:Edit"));
        awardTypePermission.AddChild(TrainingPermissions.AwardTypes.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TrainingResource>(name);
    }
}