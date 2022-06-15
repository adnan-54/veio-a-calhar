using Microsoft.AspNetCore.Mvc.Rendering;

namespace VeioACalhar.ViewModels;

public class FinalizarVendaViewModel
{
    public IReadOnlyCollection<SelectListItem>? Pagadores { get; set; }

    public IReadOnlyCollection<SelectListItem>? FormasPagamento { get; set; }

    public SelectListItem? Pagador { get; set; }

    public SelectListItem? FormaPagamento { get; set; }

    public decimal ValorRecebido { get; set; }
}
