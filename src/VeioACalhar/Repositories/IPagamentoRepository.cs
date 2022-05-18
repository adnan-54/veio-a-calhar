using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IPagamentoRepository
{
    Pagamento Create(Pagamento pagamento);

    Pagamento Get(int id);

    IReadOnlyCollection<Pagamento> GetAll();

    Pagamento Update(Pagamento pagamento);

    void Delete(Pagamento pagamento);
}
