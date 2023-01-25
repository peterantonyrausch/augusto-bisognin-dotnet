namespace Questao5.Domain.Entities;

public class Movimento
{
    public Guid IdMovimento { get; set; }
    public Guid IdContaCorrente { get; set; }
    public DateTime DataMovimento { get; set; }
    public char TipoMovimento { get; set; }
    public decimal Valor { get; set; }
}