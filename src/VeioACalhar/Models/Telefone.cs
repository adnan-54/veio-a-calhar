using System.ComponentModel.DataAnnotations.Schema;

namespace VeioACalhar.Models;

[Table("Telefones")]
public class Telefone : Entidade
{
    [Column("Numero")]
    public string? Numero { get; set; }

    [Column("Observacoes")]
    public string? Observacoes { get; set; }
}