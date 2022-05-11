namespace VeioACalhar.Models;

public abstract class Transacao : Entidade
{
    public Pagamento? Pagamento { get; set; }

    public StatusTransacao? Status { get; set; }

    public DateOnly DataCriacao { get; set; }

    public DateOnly DataFechamento { get; set; }

    public string? Observacoes { get; set; }

    public IEnumerable<ProdutoTransacao>? Produtos { get; set; }
}