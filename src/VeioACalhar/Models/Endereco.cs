namespace VeioACalhar.Models;

public class Endereco : Entidade
{
    public Pessoa Pessoa { get; set; }

    public string? Logradouro { get; set; }

    public int Numero { get; set; }

    public string? Bairro { get; set; }

    public string? Cidade { get; set; }

    public string? CEP { get; set; }

    public string? Observacoes { get; set; }
}
