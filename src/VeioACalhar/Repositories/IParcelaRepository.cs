using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IParcelaRepository
{
    IReadOnlyCollection<Parcela> CreateFrom(Pagamento pagamento);

    IReadOnlyCollection<Parcela> GetFrom(Pagamento pagamento);

    IReadOnlyCollection<Parcela> UpdateFrom(Pagamento pagamento);

    void DeleteFrom(Pagamento pagamento);
}
