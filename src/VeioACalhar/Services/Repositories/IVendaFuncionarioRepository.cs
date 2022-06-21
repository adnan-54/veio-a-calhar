using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IVendaFuncionarioRepository
{
    void CreateFor(Venda venda);

    IReadOnlyCollection<Funcionario> GetFor(Venda venda);

    void UpdateFor(Venda venda);

    void DeleteFor(Venda venda);
}