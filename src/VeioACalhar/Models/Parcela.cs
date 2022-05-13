namespace VeioACalhar.Models;

public record Parcela : Entidade
{
    public int Numero { get; init; }

    public decimal Valor { get; init; }

    public int PorcentagemDesconto { get; init; }

    public decimal ValorDesconto => Valor - Valor * PorcentagemDesconto / 100;

    public decimal ValorPago { get; init; }

    public DateOnly DataVencimento { get; init; }

    public DateOnly DataPagamento { get; init; }

    public bool Paga => DataPagamento != default;
}