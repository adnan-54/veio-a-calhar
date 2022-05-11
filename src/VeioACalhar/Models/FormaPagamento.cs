namespace VeioACalhar.Models;

public class FormaPagamento : Entidade
{
    public string? Forma { get; set; }

    public int MaximoParcelas { get; set; }
}