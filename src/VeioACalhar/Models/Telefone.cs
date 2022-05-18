namespace VeioACalhar.Models;

public record Telefone : Entidade
{
    public Telefone()
    {
        Numero = string.Empty;
        Observacoes = string.Empty;
    }

    public string Numero { get; init; }

    public string Observacoes { get; init; }
}