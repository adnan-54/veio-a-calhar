using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.Models;

public record Pagamento : Entidade
{
    public Pagamento()
    {
        Transacao = new TransacaoGenerica();
        Pagador = new PessoaGenerica();
        Favorecido = new PessoaGenerica();
        FormaPagamento = new();
        Parcelas = Enumerable.Empty<Parcela>();
    }

    [Display(Name = "Transação")]
    public Transacao Transacao { get; init; }

    [Display(Name = "Pagador")]
    public Pessoa Pagador { get; init; }

    [Display(Name = "Favorecido")]
    public Pessoa Favorecido { get; init; }

    [Display(Name = "Forma de Pagamento")]
    public FormaPagamento FormaPagamento { get; init; }

    [Display(Name = "Parcelas")]
    public IEnumerable<Parcela> Parcelas { get; init; }
}
