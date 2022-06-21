using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.ViewModels;

public class ErrorViewModel : ViewModelBase
{
    [Display(Name = "Código da Requisição")]
    public string? RequestId { get; set; }

    [Display(Name = "Mostrar Código da Requisição")]
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
