using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Exceptions;
using Questao5.Domain.Stores;

namespace Questao5.Application.Handlers;

public class CriarMovimentoHandler : IRequestHandler<CriarMovimentoRequest, CriarMovimentoResponse>
{
    private readonly IMovimentoCommandStore _commandStore;
    private readonly IContaCorrenteQueryStore _contaCorrenteQueryStore;

    public CriarMovimentoHandler(
        IMovimentoCommandStore commandStore,
        IContaCorrenteQueryStore contaCorrenteQueryStore)
    {
        _commandStore = commandStore;
        _contaCorrenteQueryStore = contaCorrenteQueryStore;
    }

    public async Task<CriarMovimentoResponse> Handle(CriarMovimentoRequest request,
        CancellationToken cancellationToken)
    {
        var resultado = await ValidarIdempotencia(request.IdRequisicao, cancellationToken)
                        ?? await CriarMovimento(request, cancellationToken);

        return new CriarMovimentoResponse(resultado);
    }

    private async Task<Guid?> ValidarIdempotencia(Guid idRequisicao, CancellationToken cancellationToken)
    {
        var idempotencia = await _commandStore.ValidarIdempotencia(idRequisicao, cancellationToken);

        return idempotencia?.ObterResultado<Guid>();
    }

    private async Task ValidarMovimento(CriarMovimentoRequest request, CancellationToken cancellationToken)
    {
        if (request.Valor <= 0)
            throw ErrorCodes.InvalidValue();

        if (request.TipoMovimento != "C" && request.TipoMovimento != "D")
            throw ErrorCodes.InvalidType();

        var conta = await _contaCorrenteQueryStore.BuscarPorId(request.IdContaCorrente, cancellationToken);
        if (conta == null)
            throw ErrorCodes.InvalidAccount();

        if (!conta.Ativo)
            throw ErrorCodes.InactiveAccount();
    }

    private async Task<Guid> CriarMovimento(CriarMovimentoRequest request, CancellationToken cancellationToken)
    {
        await ValidarMovimento(request, cancellationToken);

        var movimento = request.MapearParaNovaEntidade();
        var idempotencia = request.MapearParaIdempotencia();
        idempotencia.AdicionarResultado(movimento.IdMovimento);

        await _commandStore.InserirIdempotencia(idempotencia, cancellationToken);
        await _commandStore.InserirAsync(movimento, cancellationToken);
        await _commandStore.SalvarAsync(cancellationToken);

        return movimento.IdMovimento;
    }
}