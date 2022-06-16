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
        Observacoes = string.Empty;
    }

    public IEnumerable<ProdutoTransacao> Produtos { get; init; }

    public StatusTransacao Status { get; init; }

    public DateOnly DataCriacao { get; init; }

    public DateOnly? DataFechamento { get; init; }

    public string? Observacoes { get; init; }

    [DataType(DataType.Currency)]
    public decimal ValorTotal => Produtos.Sum(p => p.ValorTotal);
}

