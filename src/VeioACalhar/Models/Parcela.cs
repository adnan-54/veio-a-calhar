using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.Models;

public record Parcela : Entidade
{
    [Display(Name = "Numero")]
    public int Numero { get; init; }

    [DataType(DataType.Currency)]
    [Display(Name = "Valor")]
    public decimal Valor { get; init; }

    [Display(Name = "Porcentagem de Desconto")]
    public int PorcentagemDesconto { get; init; }

    [DataType(DataType.Currency)]
    [Display(Name = "Valor de Desconto")]
    public decimal ValorDesconto => Valor - Valor * PorcentagemDesconto / 100;

    [DataType(DataType.Currency)]
    [Display(Name = "Valor Pago")]
    public decimal ValorPago { get; init; }

    [DataType(DataType.Date)]
    [Display(Name = "Data de Vencimento")]
    public DateOnly DataVencimento { get; init; }

    [DataType(DataType.Date)]
    [Display(Name = "Data de Pagamento")]
    public DateOnly DataPagamento { get; init; }

    [Display(Name = "Pago")]
    public bool Paga => DataPagamento != default;
}
