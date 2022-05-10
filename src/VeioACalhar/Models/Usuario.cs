namespace VeioACalhar.Models;

public class Usuario : Entidade
{
    public string? Nome { get; init; }

    public DateOnly DataCadastro { get; init; }

    public bool Ativo { get; init; }
}