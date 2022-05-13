namespace VeioACalhar.Models;

public record Compra : Transacao
{
    public Compra()
    {
        Fornecedor = new();
    }

    public Fornecedor Fornecedor { get; init; }

    public DateOnly DataCompra { get; init; }

    public DateOnly DataEntrega { get; init; }
}
