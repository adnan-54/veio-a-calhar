namespace VeioACalhar.Models;

public class FormaPagamento : Entidade
{
    public string? Forma { get; init; }

    public int MaximoParcelas { get; init; }
}