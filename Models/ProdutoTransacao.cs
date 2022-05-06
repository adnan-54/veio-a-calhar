namespace VeioACalhar.Models;

public class ProdutoTransacao
{
    public Produto? Produto { get; init; }

    public int Quantidade { get; init; }

    public decimal ValorUnitario { get; init; }

    public int DescontoUnitario { get; init; }

    public decimal ValorTotal => (ValorUnitario - ValorUnitario * DescontoUnitario / 100) * Quantidade;
}