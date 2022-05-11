namespace VeioACalhar.Models;

public class Parcela : Entidade
{
    public int Numero { get; set; }

    public decimal Valor { get; set; }

    public int PorcentagemDesconto { get; set; }

    public decimal ValorDesconto => Valor - Valor * PorcentagemDesconto / 100;

    public decimal ValorPago { get; set; }

    public DateOnly DataVencimento { get; set; }

    public DateOnly? DataPagamento { get; set; }

    public bool Paga => DataPagamento != null;
}