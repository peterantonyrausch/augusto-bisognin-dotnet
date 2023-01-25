using Questao5.Domain.Entities;

namespace Questao5.Domain.Stores;

public interface IMovimentoQueryStore : IQueryStore<Movimento, Guid>
{
    Task<IEnumerable<Movimento>> BuscarPorContaCorrente(Guid idContaCorrente,
        CancellationToken cancellationToken = default);
}