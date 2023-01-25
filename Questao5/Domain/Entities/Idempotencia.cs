using System.Text.Json;

namespace Questao5.Domain.Entities;

public class Idempotencia
{
    public Guid ChaveIdempotencia { get; set; }
    public string Requisicao { get; set; } = null!;
    public string? Resultado { get; set; }

    public void AdicionarResultado(object resultado)
    {
        Resultado = JsonSerializer.Serialize(resultado);
    }

    public T? ObterResultado<T>()
    {
        return Resultado == null ? default : JsonSerializer.Deserialize<T>(Resultado);
    }
}