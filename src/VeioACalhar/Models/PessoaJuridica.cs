using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.Models;

public abstract record PessoaJuridica : Pessoa
{
    [StringLength(64, ErrorMessage = "O nome fantasia pode ter no maximo 64 caracteres")]
    [Display(Name = "Nome Fantasia")]
    public string? NomeFantasia { get; init; }

    [StringLength(14, ErrorMessage = "A inscrição estadual deve ter 14 caracteres")]
    [Display(Name = "Inscrição Estadual")]
    public string? InscricaoEstadual { get; init; }

    [StringLength(14, ErrorMessage = "O CNPJ deve ter 14 caracteres")]
    [Display(Name = "CNPJ")]
    public string? Cnpj { get; init; }
}
