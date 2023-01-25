namespace Questao5.Application.Commands.Responses;

public class CriarMovimentoResponse
{
    public Guid IdMovimento { get; }

    public CriarMovimentoResponse(Guid idMovimento)
    {
        IdMovimento = idMovimento;
    }
}