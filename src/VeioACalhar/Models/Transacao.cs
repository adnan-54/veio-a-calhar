using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace VeioACalhar.Models;

public abstract record Transacao : Entidade
{
    public Transacao()
    {
        Status = new();
        DataCriacao = DateOnly.FromDateTime(DateTime.Today);
        Observacoes = string.Empty;
        Produtos = Enumerable.Empty<ProdutoTransacao>();
    }

    public StatusTransacao Status { get; init; }

    public DateOnly DataCriacao { get; init; }

    public DateOnly? DataFechamento { get; init; }

    public string? Observacoes { get; init; }

    public IEnumerable<ProdutoTransacao> Produtos { get; init; }
}

