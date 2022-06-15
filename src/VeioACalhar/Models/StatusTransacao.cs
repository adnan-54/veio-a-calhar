namespace VeioACalhar.Models;

public record StatusTransacao : Entidade
{
    public StatusTransacao()
    {
        Id = 1;
        Status = "Orçamento";
    }

    public string Status { get; init; }

    public bool EmAberto => Id == 1 || Id == 2;
}
