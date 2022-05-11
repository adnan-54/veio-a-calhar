namespace VeioACalhar.Models;

public class Pagamento : Entidade
{
    public Pessoa? Pagador { get; set; }

    public Pessoa? Favorecido { get; set; }

    public FormaPagamento? FormaPagamento { get; set; }

    public IEnumerable<Parcela>? Parcelas { get; set; }
}
