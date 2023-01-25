namespace Questao5.Domain.Stores;

public interface IQueryStore<T, in TKey>
{
    Task<T?> BuscarPorId(TKey id, CancellationToken cancellationToken = default);
}