namespace VeioACalhar.Models;

public class Sale
{
    public long Id { get; }

    public Customer Customer { get; }

    public DateTime Date { get; }

    public IEnumerable<Product> Products { get; }
}
