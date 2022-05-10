namespace VeioACalhar.Models;

public class Pagamento : Entidade
{
    public Pessoa? Pagador { get; init; }

    public Pessoa? Favorecido { get; init; }

    public FormaPagamento? FormaPagamento { get; init; }

    public IEnumerable<Parcela>? Parcelas { get; init; }
}
