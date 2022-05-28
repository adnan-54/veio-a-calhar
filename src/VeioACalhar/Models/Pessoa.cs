namespace VeioACalhar.Models;

public abstract record Pessoa : Entidade
{
    public Pessoa()
    {
        Nome = string.Empty;
        Observacoes = string.Empty;
        Pix = string.Empty;
        Email = string.Empty;
        Endereco = string.Empty;
        Telefone = string.Empty;
    }

    public string Nome { get; init; }

    public string Observacoes { get; init; }

    public string Pix { get; init; }

    public string Email { get; init; }

    public string Telefone { get; init; }

    public string Endereco { get; init; }
}
