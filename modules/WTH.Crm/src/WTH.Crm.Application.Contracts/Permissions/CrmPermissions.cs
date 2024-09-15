using Volo.Abp.Reflection;

namespace Wth.Crm.Permissions;

public class CrmPermissions
{
    public const string GroupName = "Crm";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CrmPermissions));
    }

    public static class Companies
    {
        public const string Default = GroupName + ".Companies";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Addresses
    {
        public const string Default = GroupName + ".Addresses";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class CompanyLocations
    {
        public const string Default = GroupName + ".CompanyLocations";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Employees
    {
        public const string Default = GroupName + ".Employees";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class EmployeeEmails
    {
        public const string Default = GroupName + ".EmployeeEmails";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class EmployeeTelephones
    {
        public const string Default = GroupName + ".EmployeeTelephones";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class EmployeeAddresses
    {
        public const string Default = GroupName + ".EmployeeAddresses";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class CompanyAddresses
    {
        public const string Default = GroupName + ".CompanyAddresses";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class CompanyEmails
    {
        public const string Default = GroupName + ".CompanyEmails";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class CompanyTelephones
    {
        public const string Default = GroupName + ".CompanyTelephones";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Notes
    {
        public const string Default = GroupName + ".Notes";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}