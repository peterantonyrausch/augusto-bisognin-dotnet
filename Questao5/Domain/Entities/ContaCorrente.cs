﻿namespace Questao5.Domain.Entities;

public class ContaCorrente
{
    public Guid IdContaCorrente { get; set; }
    public int Numero { get; set; }
    public string Nome { get; set; } = null!;
    public bool Ativo { get; set; }
}