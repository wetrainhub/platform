using System.Threading.Tasks;

namespace WTH.Platform.Data;

public interface IPlatformDbSchemaMigrator
{
    Task MigrateAsync();
}
