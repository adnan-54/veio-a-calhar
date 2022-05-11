namespace VeioACalhar.Models;

public class Usuario : Entidade
{
    public string? Nome { get; set; }

    public DateOnly DataCadastro { get; set; }

    public bool Ativo { get; set; }
}