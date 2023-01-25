using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Stores;
using Questao5.Infrastructure.Database.CommandStore;
using Questao5.Infrastructure.Database.QueryStore;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Extensions;

public static class DatabaseExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string databaseName)
    {
        services.AddSingleton(new DatabaseConfig { Name = databaseName });
        services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
        SqlMapper.AddTypeHandler(new SqliteStringToGuidMapper());

        services.AddScoped<IDbConnection>(provider =>
        {
            var config = provider.GetRequiredService<DatabaseConfig>();
            return new SqliteConnection(config.Name);
        });
        services.AddScoped<IMovimentoCommandStore, MovimentoCommandStore>();
        services.AddScoped<IContaCorrenteQueryStore, ContaCorrenteQueryStore>();
        services.AddScoped<IMovimentoQueryStore, MovimentoQueryStore>();

        return services;
    }
}