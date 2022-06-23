using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.Models;

public record Produto : Entidade
{
    public Produto()
    {
        Fornecedor = new();
        Nome = string.Empty;
        Unidade = new();
        Descricao = string.Empty;
    }

    public Fornecedor Fornecedor { get; init; }

    [StringLength(32, MinimumLength = 3, ErrorMessage = "O nome precisa ter entre 3 e 32 caracteres")]
    [Display(Name = "Nome")]
    public string Nome { get; init; }

    [Display(Name = "Descrição")]
    public string? Descricao { get; init; }

    [DataType(DataType.Currency)]
    [Display(Name = "Preço de Custo")]
    public decimal PrecoCusto { get; init; }

    [DataType(DataType.Currency)]
    [Display(Name = "Preço de Venda")]
    public decimal PrecoVenda { get; init; }

    [Display(Name = "Quantidade")]
    public decimal Quantidade { get; init; }

    public Unidade Unidade { get; init; }
}
