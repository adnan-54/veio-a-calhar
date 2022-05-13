namespace VeioACalhar.Models;

public record Produto : Entidade
{
    public Produto()
    {
        Fornecedor = new();
        Nome = string.Empty;
        Descricao = string.Empty;
        Unidade = new();
    }

    public Fornecedor Fornecedor { get; init; }

    public string Nome { get; init; }

    public string Descricao { get; init; }

    public decimal Preco_Custo { get; init; }

    public decimal Preco_Venda { get; init; }

    public decimal Quantidade { get; init; }

    public Unidade Unidade { get; init; }
}