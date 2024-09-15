namespace Wth.Crm;

public static class CrmDbProperties
{
    public static string DbTablePrefix { get; set; } = "Crm";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Crm";
}
