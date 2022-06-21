using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace VeioACalhar.Models;

public abstract record Transacao : Entidade
{
    public Transacao()
    {
        Produtos = Enumerable.Empty<ProdutoTransacao>();
        Status = new();
        DataCriacao = DateOnly.FromDateTime(DateTime.Today);
    }

    [Display(Name = "Produtos")]
    public IEnumerable<ProdutoTransacao> Produtos { get; init; }

    public StatusTransacao Status { get; init; }

    [DataType(DataType.Date)]
    [Display(Name = "Data de Criação")]
    public DateOnly DataCriacao { get; init; }

    [DataType(DataType.Date)]
    [Display(Name = "Data de Fechamento")]
    public DateOnly? DataFechamento { get; init; }

    [DataType(DataType.MultilineText)]
    [Display(Name = "Observações")]
    public string? Observacoes { get; init; }

    [DataType(DataType.Currency)]
    [Display(Name = "Valor Total")]
    public decimal ValorTotal => Produtos.Sum(p => p.ValorTotal);
}

