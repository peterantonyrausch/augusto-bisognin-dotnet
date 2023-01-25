using System;

namespace Questao1;

public class ContaBancaria
{
    public int Numero { get; }
    public string Titular { get; private set; }
    public decimal Saldo { get; private set; }
    public decimal DepositoInicial { get; }

    public ContaBancaria(int numero, string titular)
    {
        Numero = numero;
        Titular = titular;
    }

    public ContaBancaria(int numero, string titular, decimal depositoInicial)
    {
        Numero = numero;
        Titular = titular;
        Saldo = depositoInicial;
        DepositoInicial = depositoInicial;
    }

    public void Deposito(decimal quantia)
    {
        Saldo += quantia;
    }

    public void Saque(decimal quantia)
    {
        Saldo -= quantia;
    }

    public void AtualizarNome(string nome)
    {
        Titular = nome;
    }

    public override string ToString()
    {
        return $"Conta {Numero}, Titular: {Titular}, Saldo: $ {Saldo}";
    }
}