namespace VeioACalhar.Models;

public record FormaPagamento : Entidade
{
    public FormaPagamento()
    {
        Nome = string.Empty;
    }

    public string Nome { get; init; }

    public int MaximoParcelas { get; init; }
}