using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.Models;

public record ProdutoTransacao : Entidade
{
    public ProdutoTransacao()
    {
        Produto = new();
    }

    public Produto Produto { get; init; }

    [Range(0, double.MaxValue)]
    public decimal Quantidade { get; init; }

    [DataType(DataType.Currency)]
    public decimal ValorUnitario { get; init; }

    [Range(0, 100)]
    public int DescontoUnitario { get; init; }

    [DataType(DataType.Currency)]
    public decimal ValorTotal => (ValorUnitario - ValorUnitario * DescontoUnitario / 100) * Quantidade;
}
