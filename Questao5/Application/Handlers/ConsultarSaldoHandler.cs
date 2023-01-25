using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Exceptions;
using Questao5.Domain.Stores;

namespace Questao5.Application.Handlers;

public class ConsultarSaldoHandler : IRequestHandler<ConsultarSaldoQueryRequest, ConsultarSaldoQueryResponse>
{
    private readonly IContaCorrenteQueryStore _contaCorrenteQueryStore;
    private readonly IMovimentoQueryStore _movimentoQueryStore;

    public ConsultarSaldoHandler(
        IContaCorrenteQueryStore contaCorrenteQueryStore,
        IMovimentoQueryStore movimentoQueryStore)
    {
        _contaCorrenteQueryStore = contaCorrenteQueryStore;
        _movimentoQueryStore = movimentoQueryStore;
    }

    public async Task<ConsultarSaldoQueryResponse> Handle(ConsultarSaldoQueryRequest request,
        CancellationToken cancellationToken)
    {
        var conta = await BuscarEValidarConta(request.IdContaCorrente, cancellationToken);

        var movimentos =
            (await _movimentoQueryStore.BuscarPorContaCorrente(request.IdContaCorrente, cancellationToken)).ToArray();

        var totalCreditos = movimentos.Where(movimento => movimento.TipoMovimento == 'C')
            .Aggregate(0m, (total, movimento) => total + movimento.Valor);

        var totalDebitos = movimentos.Where(movimento => movimento.TipoMovimento == 'D')
            .Aggregate(0m, (total, movimento) => total + movimento.Valor);

        return new ConsultarSaldoQueryResponse(
            conta.Numero, conta.Nome, DateTime.Now, totalCreditos - totalDebitos);
    }

    private async Task<ContaCorrente> BuscarEValidarConta(Guid idContaCorrente, CancellationToken cancellationToken)
    {
        var conta = await _contaCorrenteQueryStore.BuscarPorId(idContaCorrente, cancellationToken);
        if (conta == null)
            throw ErrorCodes.InvalidAccount();

        if (!conta.Ativo)
            throw ErrorCodes.InactiveAccount();

        return conta;
    }
}