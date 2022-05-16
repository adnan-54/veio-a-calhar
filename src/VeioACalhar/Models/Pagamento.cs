namespace VeioACalhar.Models;

public record Pagamento : Entidade
{
    public Pagamento()
    {
        Pagador = new PessoaPadrao();
        Favorecido = new PessoaPadrao();
        FormaPagamento = new();
        Parcelas = Enumerable.Empty<Parcela>();
    }

    public Pessoa Pagador { get; init; }

    public Pessoa Favorecido { get; init; }

    public FormaPagamento FormaPagamento { get; init; }

    public IEnumerable<Parcela> Parcelas { get; init; }
}
