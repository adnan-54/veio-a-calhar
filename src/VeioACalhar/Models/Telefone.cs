namespace VeioACalhar.Models;

public class Telefone : Entidade
{
    public Pessoa? Pessoa { get; set; }

    public string? Numero { get; set; }

    public string? Observacoes { get; set; }
}