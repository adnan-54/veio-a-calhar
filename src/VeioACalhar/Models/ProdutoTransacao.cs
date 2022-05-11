namespace VeioACalhar.Models;

public class ProdutoTransacao : Entidade
{
    public Produto? Produto { get; set; }

    public int Quantidade { get; set; }

    public decimal ValorUnitario { get; set; }

    public int DescontoUnitario { get; set; }

    public decimal ValorTotal => (ValorUnitario - ValorUnitario * DescontoUnitario / 100) * Quantidade;
}