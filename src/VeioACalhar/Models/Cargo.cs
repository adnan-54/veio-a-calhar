namespace VeioACalhar.Models;

public record Cargo : Entidade
{
    public Cargo()
    {
        Nome = string.Empty;
    }

    public string Nome { get; init; }
}
