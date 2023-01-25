using Questao5.Domain.Entities;

namespace Questao5.Domain.Stores;

public interface IMovimentoCommandStore : ICommandStore
{
    Task InserirAsync(Movimento value, CancellationToken cancellationToken = default);
}