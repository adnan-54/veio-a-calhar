using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.Models;

public record Unidade : Entidade
{
    public Unidade()
    {
        Nome = string.Empty;
        Sigla = string.Empty;
    }

    [StringLength(32, ErrorMessage = "O nome pode ter no maximo 32 caracteres")]
    [Display(Name = "Nome")]
    public string Nome { get; init; }

    [StringLength(8, ErrorMessage = "A sigla pode ter no maximo 8 caracteres")]
    [Display(Name = "Sigla")]
    public string Sigla { get; init; }
}
