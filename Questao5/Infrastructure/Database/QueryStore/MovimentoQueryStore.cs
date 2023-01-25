using System.Data;
using Questao5.Domain.Entities;
using Questao5.Domain.Stores;

namespace Questao5.Infrastructure.Database.QueryStore;

public class MovimentoQueryStore : QueryStore<Movimento, Guid>, IMovimentoQueryStore
{
    public MovimentoQueryStore(IDbConnection connection) : base(connection)
    {
    }

    protected override string NomeTabela => "movimento";
    protected override string NomeChavePrimaria => "idmovimento";
    public Task<IEnumerable<Movimento>> BuscarPorContaCorrente(Guid idContaCorrente, CancellationToken cancellationToken = default)
    {
        return BuscarPorColuna("idcontacorrente", idContaCorrente, cancellationToken);
    }
}