using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IVendaClienteRepository
{
    void CreateFor(Venda venda);

    IReadOnlyCollection<Cliente> GetFor(Venda venda);

    void UpdateFor(Venda venda);

    void DeleteFor(Venda vendas);
}
