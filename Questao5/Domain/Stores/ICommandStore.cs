using Questao5.Domain.Entities;

namespace Questao5.Domain.Stores;

public interface ICommandStore
{
    Task InserirIdempotencia(Idempotencia idempotencia, CancellationToken cancellationToken = default);
    Task<Idempotencia?> ValidarIdempotencia(Guid chave, CancellationToken cancellationToken = default);
    Task SalvarAsync(CancellationToken cancellationToken = default);
}