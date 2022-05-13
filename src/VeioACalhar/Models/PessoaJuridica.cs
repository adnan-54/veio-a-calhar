namespace VeioACalhar.Models;

public record PessoaJuridica : Pessoa
{
    public PessoaJuridica()
    {
        NomeFantasia = string.Empty;
        InscricaoEstadual = string.Empty;
        Cnpj = string.Empty;
    }

    public string NomeFantasia { get; init; }

    public string InscricaoEstadual { get; init; }

    public string Cnpj { get; init; }
}
