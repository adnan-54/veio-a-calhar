using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.Models;

public record Cargo : Entidade
{
    public Cargo()
    {
        Nome = string.Empty;
    }

    [StringLength(32, MinimumLength = 1, ErrorMessage = "O cargo precisa ter entre 1 e 32 caracteres")]
    [Display(Name = "Nome")]
    public string Nome { get; init; }
}
