namespace VeioACalhar.Models;

public record Telefone : Entidade
{
    public Telefone()
    {
        Pessoa = new PessoaPadrao();
        Numero = string.Empty;
        Observacoes = string.Empty;
    }

    public Pessoa Pessoa { get; init; }

    public string Numero { get; init; }

    public string Observacoes { get; init; }
}