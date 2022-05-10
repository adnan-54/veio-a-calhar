namespace VeioACalhar.Models;

public class Compra : Transacao
{
    public Fornecedor? Fornecedor { get; init; }

    public DateOnly DataCompra { get; init; }

    public DateOnly DataEntrega { get; init; }
}
