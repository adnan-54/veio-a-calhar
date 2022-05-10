using System.ComponentModel.DataAnnotations.Schema;

namespace VeioACalhar.Models;

[Table("Telefones")]
public class Telefone : Entidade
{
    [Column("Numero")]
    public string? Numero { get; init; }

    [Column("Observacoes")]
    public string? Observacoes { get; init; }
}