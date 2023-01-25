using System.Data;
using Questao5.Domain.Entities;
using Questao5.Domain.Stores;

namespace Questao5.Infrastructure.Database.QueryStore;

public class ContaCorrenteQueryStore : QueryStore<ContaCorrente, Guid>, IContaCorrenteQueryStore
{
    public ContaCorrenteQueryStore(IDbConnection connection) : base(connection)
    {
    }

    protected override string NomeTabela => "contacorrente";
    protected override string NomeChavePrimaria => "idcontacorrente";
}