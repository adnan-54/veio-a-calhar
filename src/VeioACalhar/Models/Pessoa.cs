namespace VeioACalhar.Models;

public abstract class Pessoa : Entidade
{
    public string? Nome { get; set; }

    public string? Observacoes { get; set; }

    public string? PIX { get; set; }

    public string? Email { get; set; }

    public IEnumerable<Endereco>? Enderecos { get; set; }

    public IEnumerable<Telefone>? Telefones { get; set; }
}
