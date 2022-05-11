namespace VeioACalhar.Models;

public class Compra : Transacao
{
    public Fornecedor? Fornecedor { get; set; }

    public DateOnly DataCompra { get; set; }

    public DateOnly DataEntrega { get; set; }
}
