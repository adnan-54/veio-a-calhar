namespace VeioACalhar.Models;

public abstract class PessoaJuridica : Pessoa
{
    public string? NomeFantasia { get; set; }

    public string? InscricaoEstadual { get; set; }

    public string? CNPJ { get; set; }
}
