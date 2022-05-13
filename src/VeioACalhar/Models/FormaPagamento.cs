namespace VeioACalhar.Models;

public record FormaPagamento : Entidade
{
    public FormaPagamento()
    {
        Forma = string.Empty;
    }

    public string Forma { get; init; }

    public int MaximoParcelas { get; init; }
}