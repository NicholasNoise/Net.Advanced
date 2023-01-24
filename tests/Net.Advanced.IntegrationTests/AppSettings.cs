using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Net.Advanced.IntegrationTests;
public static class AppSettings
{
  private const string PostgreSqlConnectionString = "PostgreSqlConnection";

  private const string AppSettingFilename = "appsettings.json";

  public static IConfiguration InitConfiguration()
  {
    var config = new ConfigurationBuilder()
      .AddJsonFile(AppSettingFilename)
      .AddEnvironmentVariables()
      .Build();
    return config;
  }

  public static string GetUniquePostgreSqlConnectionString(IConfiguration config, string dbName, char separator = '_')
  {
    var orgConnect = config.GetConnectionString(PostgreSqlConnectionString);
    if (string.IsNullOrEmpty(orgConnect))
    {
      throw new InvalidOperationException($"Your {AppSettingFilename} file isn't set up for the '{PostgreSqlConnectionString}'.");
    }

    var builder = new NpgsqlConnectionStringBuilder(orgConnect);

    var extraDatabaseName = $"{separator}{dbName}";

    builder.Database += extraDatabaseName;

    if (builder.Database.Length > 64)
    {
      throw new InvalidOperationException("PostgreSQL database names are limited to 64 chars, " +
                                          $"but your database name '{builder.Database}' is {builder.Database.Length} chars. " +
                                          $"Consider shortening the name in the '{PostgreSqlConnectionString}' in your {AppSettingFilename} file or stop adding a extra name on the end");
    }

    return builder.ToString();
  }
}
