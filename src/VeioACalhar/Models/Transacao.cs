namespace VeioACalhar.Models;

public record Transacao : Entidade
{
    public Transacao()
    {
        Pagamento = new();
        Status = new();
        Observacoes = string.Empty;
        Produtos = Enumerable.Empty<ProdutoTransacao>();
    }

    public Pagamento Pagamento { get; init; }

    public StatusTransacao Status { get; init; }

    public DateOnly DataCriacao { get; init; }

    public DateOnly DataFechamento { get; init; }

    public string Observacoes { get; init; }

    public IEnumerable<ProdutoTransacao> Produtos { get; init; }
}