using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;

namespace Questao5.Application.Commands.Requests;

public class CriarMovimentoRequest : IRequest<CriarMovimentoResponse>
{
    [Required]
    public Guid IdRequisicao { get; set; }

    [Required]
    public Guid IdContaCorrente { get; set; }

    [Required]
    public decimal Valor { get; set; }

    [Required]
    [StringLength(1, MinimumLength = 1)]
    public string TipoMovimento { get; set; } = null!;

    public Movimento MapearParaNovaEntidade() =>
        new()
        {
            IdMovimento = Guid.NewGuid(),
            IdContaCorrente = IdContaCorrente,
            DataMovimento = DateTime.Now,
            Valor = Valor,
            TipoMovimento = TipoMovimento[0],
        };

    public Idempotencia MapearParaIdempotencia() =>
        new()
        {
            ChaveIdempotencia = IdRequisicao,
            Requisicao = JsonSerializer.Serialize(this),
        };
}