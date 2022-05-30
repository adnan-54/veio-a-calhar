namespace VeioACalhar.Models;

public record Usuario : Entidade
{
    public Usuario()
    {
        Login = string.Empty;
        Password = string.Empty;
    }

    public string Login { get; init; }

    public string Password { get; init; }

    public DateOnly DataCadastro { get; init; }

    public bool Ativo { get; init; }
}