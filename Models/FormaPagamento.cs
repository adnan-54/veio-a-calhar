namespace VeioACalhar.Models;

public class FormaPagamento
{
    public int Id { get; init; }

    public string? Forma { get; init; }

    public int MaximoParcelas { get; init; }
}