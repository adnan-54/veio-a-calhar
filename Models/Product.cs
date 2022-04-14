using VeioACalhar.Enums;

namespace VeioACalhar.Models;

public class Product
{
    public long Id { get; }

    public string Name { get; }

    public string Description { get; }

    public double Price { get; }

    public double Quantity { get; }

    public Unity Unity { get; }
}
