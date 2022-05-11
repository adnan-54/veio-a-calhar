namespace VeioACalhar.Models;

public class Produto : Entidade
{
    public Fornecedor? Fornecedor { get; set; }

    public string? Nome { get; set; }

    public string? Descricao { get; set; }

    public decimal Preco_Custo { get; set; }

    public decimal Preco_Venda { get; set; }

    public decimal Quantidade { get; set; }

    public Unidade? Unidade { get; set; }
}