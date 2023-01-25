namespace Questao5.Application.Queries.Responses;

public class ConsultarSaldoQueryResponse
{
    public ConsultarSaldoQueryResponse(int numeroConta, string nomeTitular, DateTime horaDaConsulta, decimal saldo)
    {
        NumeroConta = numeroConta;
        NomeTitular = nomeTitular;
        HoraDaConsulta = horaDaConsulta;
        Saldo = saldo;
    }

    public int NumeroConta { get; }
    public string NomeTitular { get; }
    public DateTime HoraDaConsulta { get; }
    public decimal Saldo { get; }
}