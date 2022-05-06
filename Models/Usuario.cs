namespace VeioACalhar.Models;

public class Usuario
{
    public int Id { get; init; }

    public string? Nome { get; init; }

    public DateOnly DataCadastro { get; init; }

    public bool Ativo { get; init; }
}