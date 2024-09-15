namespace WTH.Training;

public static class TrainingDbProperties
{
    public static string DbTablePrefix { get; set; } = "Training";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Training";
}
