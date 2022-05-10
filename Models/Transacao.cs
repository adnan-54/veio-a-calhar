namespace VeioACalhar.Models;

public abstract class Transacao : Entidade
{
    public Pagamento? Pagamento { get; init; }

    public StatusTransacao? Status { get; init; }

    public DateOnly DataCriacao { get; init; }

    public DateOnly DataFechamento { get; init; }

    public string? Observacoes { get; init; }

    public IEnumerable<ProdutoTransacao>? Produtos { get; init; }
}