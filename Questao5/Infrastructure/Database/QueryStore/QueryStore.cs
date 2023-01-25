using System.Data;
using Dapper;
using Questao5.Domain.Stores;

namespace Questao5.Infrastructure.Database.QueryStore;

public abstract class QueryStore<T, TKey> : IQueryStore<T, TKey>, IDisposable
{
    private readonly IDbConnection _connection;

    protected QueryStore(IDbConnection connection)
    {
        _connection = connection;
    }

    protected abstract string NomeTabela { get; }
    protected abstract string NomeChavePrimaria { get; }

    public async Task<T?> BuscarPorId(TKey id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var sql = $"SELECT * FROM {NomeTabela} WHERE {NomeChavePrimaria} = @id";
        return (await _connection.QueryAsync<T>(sql, new { id })).FirstOrDefault();
    }

    protected async Task<IEnumerable<T>> BuscarPorColuna(string nomeColuna, object valor,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var sql = $"SELECT * FROM {NomeTabela} WHERE {nomeColuna} = @valor";
        return await _connection.QueryAsync<T>(sql, new { valor });
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _connection.Dispose();
    }
}