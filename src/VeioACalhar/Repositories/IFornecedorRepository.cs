using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IFornecedorRepository
{
    Fornecedor Create(Fornecedor fornecedor);

    Fornecedor Get(int id);

    IEnumerable<Fornecedor> GetAll();

    Fornecedor Update(Fornecedor fornecedor);

    void Delete(Fornecedor fornecedor);
}
