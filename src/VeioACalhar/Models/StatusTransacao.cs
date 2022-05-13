namespace VeioACalhar.Models;

public record StatusTransacao : Entidade
{
    public StatusTransacao()
    {
        Status = string.Empty;
    }

    public string Status { get; init; }
}
