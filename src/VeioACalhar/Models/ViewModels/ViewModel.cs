using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.ViewModels;

public abstract class ViewModelBase
{
    [Display(Name = "Erro")]
    public string? Error { get; init; }
}
