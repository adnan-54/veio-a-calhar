namespace VeioACalhar.Models;

public abstract class PessoaJuridica : Pessoa
{
    public string? NomeFantasia { get; init; }

    public string? InscricaoEstadual { get; init; }

    public string? CNPJ { get; init; }
}
