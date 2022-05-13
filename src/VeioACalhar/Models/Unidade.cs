namespace VeioACalhar.Models;

public record Unidade : Entidade
{
    public Unidade()
    {
        Nome = string.Empty;
        Sigla = string.Empty;
    }

    public string Nome { get; init; }

    public string Sigla { get; init; }
}