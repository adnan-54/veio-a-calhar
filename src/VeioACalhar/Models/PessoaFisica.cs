namespace VeioACalhar.Models;

public abstract class PessoaFisica : Pessoa
{
    public string? CPF { get; set; }

    public string? RG { get; set; }
}
