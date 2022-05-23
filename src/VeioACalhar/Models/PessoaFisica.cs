namespace VeioACalhar.Models;

public abstract record PessoaFisica : Pessoa
{
    public PessoaFisica()
    {
        Cpf = string.Empty;
        Rg = string.Empty;
    }

    public string Cpf { get; init; }

    public string Rg { get; init; }
}
