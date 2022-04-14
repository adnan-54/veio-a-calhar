using VeioACalhar.Enums;

namespace VeioACalhar.Models;

public abstract class Person
{
    public int Id { get; }

    public string Name { get; }
    
    public string Document { get; }
    
    public DocumentType DocumentType { get; }
    
    public string Phone { get; }
    
    public string Address { get; }
}

