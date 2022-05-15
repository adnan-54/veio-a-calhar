namespace VeioACalhar.Models;

public abstract record Pessoa : Entidade
{
    public Pessoa()
    {
        Nome = string.Empty;
        Observacoes = string.Empty;
        Pix = string.Empty;
        Email = string.Empty;
        Enderecos = Enumerable.Empty<Endereco>();
        Telefones = Enumerable.Empty<Telefone>();
    }

    public string Nome { get; init; }

    public string Observacoes { get; init; }

    public string Pix { get; init; }

    public string Email { get; init; }

    public IEnumerable<Endereco> Enderecos { get; init; }

    public IEnumerable<Telefone> Telefones { get; init; }
}
