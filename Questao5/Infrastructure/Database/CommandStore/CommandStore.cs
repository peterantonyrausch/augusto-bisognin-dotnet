using System.Data;
using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Stores;

namespace Questao5.Infrastructure.Database.CommandStore;

public abstract class CommandStore : ICommandStore, IDisposable
{
    private IDbConnection Connection { get; }
    private IDbTransaction? _transaction;

    protected CommandStore(IDbConnection connection)
    {
        Connection = connection;
    }

    protected async Task ExecuteAsync<T>(string sql, T value, CancellationToken cancellationToken = default)
    {
        await EnsureHasActiveTransaction(cancellationToken);
        await Task.Run(() => Connection.ExecuteAsync(sql, value, _transaction), cancellationToken);
    }

    public async Task<Idempotencia?> ValidarIdempotencia(Guid chave, CancellationToken cancellationToken = default)
    {
        var idempotencia = await Task.Run(() => Connection.QueryFirstOrDefaultAsync<Idempotencia>(
            "SELECT resultado FROM idempotencia WHERE chave_idempotencia = @chave",
            new { chave }), cancellationToken);

        return idempotencia;
    }

    public async Task InserirIdempotencia(Idempotencia idempotencia, CancellationToken cancellationToken = default)
    {
        await ExecuteAsync(
            "INSERT INTO idempotencia VALUES (" +
            $"@{nameof(Idempotencia.ChaveIdempotencia)}," +
            $"@{nameof(Idempotencia.Requisicao)}," +
            $"@{nameof(idempotencia.Resultado)}" +
            ")",
            idempotencia, cancellationToken);
    }

    public Task SalvarAsync(CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            if (Connection.State == ConnectionState.Broken)
                return;

            _transaction?.Commit();
            _transaction = null;
        }, cancellationToken);
    }

    private async Task EnsureHasActiveTransaction(CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
            _transaction ??= Connection.BeginTransaction();
        }, cancellationToken);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        Connection.Dispose();
        _transaction?.Dispose();
    }
}