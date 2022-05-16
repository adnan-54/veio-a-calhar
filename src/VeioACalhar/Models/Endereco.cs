namespace VeioACalhar.Models;

public record Endereco : Entidade
{
    public Endereco()
    {
        Pessoa = new PessoaPadrao();
        Logradouro = string.Empty;
        Bairro = string.Empty;
        Cidade = string.Empty;
        Estado = string.Empty;
        Cep = string.Empty;
        Observacoes = string.Empty;
    }

    public Pessoa Pessoa { get; init; }

    public string Logradouro { get; init; }

    public int Numero { get; init; }

    public string Bairro { get; init; }

    public string Cidade { get; init; }

    public string Estado { get; init; }

    public string Cep { get; init; }

    public string Observacoes { get; init; }
}
