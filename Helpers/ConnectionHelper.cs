using MySqlConnector;

public static class ConnectionHelper
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        return string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);
    }

    // Build the connection string from the environment (Railway, Heroku, etc.)
    private static string BuildConnectionString(string databaseUrl)
    {
        var databaseUri = new Uri(databaseUrl);
        var userInfo = databaseUri.UserInfo.Split(':');
        var builder = new MySqlConnectionStringBuilder
        {
            Server = databaseUri.Host,
            Port = (uint)databaseUri.Port,
            UserID = userInfo[0],
            Password = userInfo[1],
            Database = databaseUri.LocalPath.TrimStart('/'),
            SslMode = MySqlSslMode.Required // Make sure to use SSL mode
        };
        return builder.ToString();
    }
}