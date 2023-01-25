using System.ComponentModel.DataAnnotations;
using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests;

public class ConsultarSaldoQueryRequest : IRequest<ConsultarSaldoQueryResponse>
{
    [Required]
    public Guid IdContaCorrente { get; set; }
}