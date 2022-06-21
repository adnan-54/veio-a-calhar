using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.Models;

public record StatusTransacao : Entidade
{
    public StatusTransacao()
    {
        Id = 1;
        Status = "Orçamento";
    }

    [StringLength(32, ErrorMessage = "O status pode ter no maximo 32 caracteres")]
    [Display(Name = "Status")]
    public string Status { get; init; }

    [Display(Name = "Em Aberto")]
    public bool EmAberto => Id == 1 || Id == 2;
}
