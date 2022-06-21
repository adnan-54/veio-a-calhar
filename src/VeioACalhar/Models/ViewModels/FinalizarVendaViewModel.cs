using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VeioACalhar.ViewModels;

public class FinalizarVendaViewModel : ViewModelBase
{
    [Display(Name = "Pagadores")]
    public IReadOnlyCollection<SelectListItem>? Pagadores { get; set; }

    [Display(Name = "Formas de Pagamento")]
    public IReadOnlyCollection<SelectListItem>? FormasPagamento { get; set; }

    [Display(Name = "Pagador")]
    public SelectListItem? Pagador { get; set; }

    [Display(Name = "Forma de Pagamento")]
    public SelectListItem? FormaPagamento { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Valor Recebido")]
    public decimal ValorRecebido { get; set; }
}
