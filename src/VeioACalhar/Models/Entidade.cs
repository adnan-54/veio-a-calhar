using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.Models;

public abstract record Entidade
{
    [Display(Name = "Código")]
    public int Id { get; init; }
}
