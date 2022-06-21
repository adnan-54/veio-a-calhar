using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.Models;

public abstract record PessoaFisica : Pessoa
{
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 caracteres")]
    [Display(Name = "CPF")]
    public string? Cpf { get; init; }

    [StringLength(12, MinimumLength = 12, ErrorMessage = "O RG deve ter 12 caracteres")]
    [Display(Name = "RG")]
    public string? Rg { get; init; }
}
