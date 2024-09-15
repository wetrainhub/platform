using Wth.Crm.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Wth.Crm.Permissions;

public class CrmPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CrmPermissions.GroupName, L("Permission:Crm"));

        var companyPermission = myGroup.AddPermission(CrmPermissions.Companies.Default, L("Permission:Companies"));
        companyPermission.AddChild(CrmPermissions.Companies.Create, L("Permission:Create"));
        companyPermission.AddChild(CrmPermissions.Companies.Edit, L("Permission:Edit"));
        companyPermission.AddChild(CrmPermissions.Companies.Delete, L("Permission:Delete"));

        var addressPermission = myGroup.AddPermission(CrmPermissions.Addresses.Default, L("Permission:Addresses"));
        addressPermission.AddChild(CrmPermissions.Addresses.Create, L("Permission:Create"));
        addressPermission.AddChild(CrmPermissions.Addresses.Edit, L("Permission:Edit"));
        addressPermission.AddChild(CrmPermissions.Addresses.Delete, L("Permission:Delete"));

        var companyLocationPermission = myGroup.AddPermission(CrmPermissions.CompanyLocations.Default, L("Permission:CompanyLocations"));
        companyLocationPermission.AddChild(CrmPermissions.CompanyLocations.Create, L("Permission:Create"));
        companyLocationPermission.AddChild(CrmPermissions.CompanyLocations.Edit, L("Permission:Edit"));
        companyLocationPermission.AddChild(CrmPermissions.CompanyLocations.Delete, L("Permission:Delete"));

        var employeePermission = myGroup.AddPermission(CrmPermissions.Employees.Default, L("Permission:Employees"));
        employeePermission.AddChild(CrmPermissions.Employees.Create, L("Permission:Create"));
        employeePermission.AddChild(CrmPermissions.Employees.Edit, L("Permission:Edit"));
        employeePermission.AddChild(CrmPermissions.Employees.Delete, L("Permission:Delete"));

        var employeeEmailPermission = myGroup.AddPermission(CrmPermissions.EmployeeEmails.Default, L("Permission:EmployeeEmails"));
        employeeEmailPermission.AddChild(CrmPermissions.EmployeeEmails.Create, L("Permission:Create"));
        employeeEmailPermission.AddChild(CrmPermissions.EmployeeEmails.Edit, L("Permission:Edit"));
        employeeEmailPermission.AddChild(CrmPermissions.EmployeeEmails.Delete, L("Permission:Delete"));

        var employeeTelephonePermission = myGroup.AddPermission(CrmPermissions.EmployeeTelephones.Default, L("Permission:EmployeeTelephones"));
        employeeTelephonePermission.AddChild(CrmPermissions.EmployeeTelephones.Create, L("Permission:Create"));
        employeeTelephonePermission.AddChild(CrmPermissions.EmployeeTelephones.Edit, L("Permission:Edit"));
        employeeTelephonePermission.AddChild(CrmPermissions.EmployeeTelephones.Delete, L("Permission:Delete"));

        var employeeAddressPermission = myGroup.AddPermission(CrmPermissions.EmployeeAddresses.Default, L("Permission:EmployeeAddresses"));
        employeeAddressPermission.AddChild(CrmPermissions.EmployeeAddresses.Create, L("Permission:Create"));
        employeeAddressPermission.AddChild(CrmPermissions.EmployeeAddresses.Edit, L("Permission:Edit"));
        employeeAddressPermission.AddChild(CrmPermissions.EmployeeAddresses.Delete, L("Permission:Delete"));

        var companyAddressPermission = myGroup.AddPermission(CrmPermissions.CompanyAddresses.Default, L("Permission:CompanyAddresses"));
        companyAddressPermission.AddChild(CrmPermissions.CompanyAddresses.Create, L("Permission:Create"));
        companyAddressPermission.AddChild(CrmPermissions.CompanyAddresses.Edit, L("Permission:Edit"));
        companyAddressPermission.AddChild(CrmPermissions.CompanyAddresses.Delete, L("Permission:Delete"));

        var companyEmailPermission = myGroup.AddPermission(CrmPermissions.CompanyEmails.Default, L("Permission:CompanyEmails"));
        companyEmailPermission.AddChild(CrmPermissions.CompanyEmails.Create, L("Permission:Create"));
        companyEmailPermission.AddChild(CrmPermissions.CompanyEmails.Edit, L("Permission:Edit"));
        companyEmailPermission.AddChild(CrmPermissions.CompanyEmails.Delete, L("Permission:Delete"));

        var companyTelephonePermission = myGroup.AddPermission(CrmPermissions.CompanyTelephones.Default, L("Permission:CompanyTelephones"));
        companyTelephonePermission.AddChild(CrmPermissions.CompanyTelephones.Create, L("Permission:Create"));
        companyTelephonePermission.AddChild(CrmPermissions.CompanyTelephones.Edit, L("Permission:Edit"));
        companyTelephonePermission.AddChild(CrmPermissions.CompanyTelephones.Delete, L("Permission:Delete"));

        var notePermission = myGroup.AddPermission(CrmPermissions.Notes.Default, L("Permission:Notes"));
        notePermission.AddChild(CrmPermissions.Notes.Create, L("Permission:Create"));
        notePermission.AddChild(CrmPermissions.Notes.Edit, L("Permission:Edit"));
        notePermission.AddChild(CrmPermissions.Notes.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CrmResource>(name);
    }
}