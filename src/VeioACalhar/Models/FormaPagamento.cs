using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.Models;

public record FormaPagamento : Entidade
{
    public FormaPagamento()
    {
        Nome = string.Empty;
    }

    [StringLength(32, ErrorMessage = "O nome pode ter no maximo 32 caracteres")]
    [Display(Name = "Nome")]
    public string Nome { get; init; }

    [Display(Name = "Maximo de Parcelas")]
    public int MaximoParcelas { get; init; }
}
