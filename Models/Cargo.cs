using System.ComponentModel.DataAnnotations.Schema;

namespace VeioACalhar.Models;

[Table("Cargos")]
public class Cargo : Entidade
{
    [Column("Nome")]
    public string? Nome { get; init; }
}
