using System.Data;
using Questao5.Domain.Entities;
using Questao5.Domain.Stores;

namespace Questao5.Infrastructure.Database.CommandStore;

public class MovimentoCommandStore : CommandStore, IMovimentoCommandStore
{
    public MovimentoCommandStore(IDbConnection connection) : base(connection)
    {
    }

    public async Task InserirAsync(Movimento value, CancellationToken cancellationToken = default)
    {
        await ExecuteAsync(
            "INSERT INTO movimento VALUES (" +
            $"@{nameof(Movimento.IdMovimento)}," +
            $"@{nameof(Movimento.IdContaCorrente)}," +
            $"@{nameof(Movimento.DataMovimento)}," +
            $"@{nameof(Movimento.TipoMovimento)}," +
            $"@{nameof(Movimento.Valor)}" +
            ")",
            value, cancellationToken);
    }
}