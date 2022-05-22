using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IVendaRepository
{
    Venda Create(Venda venda);

    Venda Get(int id);

    IReadOnlyCollection<Venda> GetAll();

    Venda Update(Venda venda);

    void Delete(Venda venda);
}