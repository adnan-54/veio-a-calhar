using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.Models;

public record ProdutoTransacao : Entidade
{
    public ProdutoTransacao()
    {
        Produto = new();
    }

    public Produto Produto { get; init; }

    [Display(Name = "Quantidade")]
    public decimal Quantidade { get; init; }

    [DataType(DataType.Currency)]
    [Display(Name = "Valor Unitário")]
    public decimal ValorUnitario { get; init; }

    [Range(0, 100, ErrorMessage = "O desconto precisa estar entre 0 e 100")]
    [Display(Name = "Desconto Unitário")]
    public int DescontoUnitario { get; init; }

    [DataType(DataType.Currency)]
    [Display(Name = "Valor Total")]
    public decimal ValorTotal => (ValorUnitario - ValorUnitario * DescontoUnitario / 100) * Quantidade;
}
