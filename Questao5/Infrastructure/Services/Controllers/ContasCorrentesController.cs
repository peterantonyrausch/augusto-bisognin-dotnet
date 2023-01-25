using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries.Requests;

namespace Questao5.Infrastructure.Services.Controllers;

[ApiController]
[Route("contas-correntes")]
public class ContasCorrentesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContasCorrentesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{idContaCorrente:guid}/saldo")]
    public async Task<IActionResult> ConsultarSaldo(Guid idContaCorrente, CancellationToken cancellationToken)
    {
        var request = new ConsultarSaldoQueryRequest { IdContaCorrente = idContaCorrente };
        var result = await _mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}