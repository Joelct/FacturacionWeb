using Microsoft.EntityFrameworkCore;

public static class DataHelper
{
    public static async Task ManageDataAsync(IServiceProvider svcProvider)
    {
        // Service: Get an instance of your DbContext
        var dbContextSvc = svcProvider.GetRequiredService<DbContext>();

        // Migration: Programmatically equivalent to Update-Database
        await dbContextSvc.Database.MigrateAsync();
    }
}
