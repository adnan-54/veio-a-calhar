using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IPessoaRepository<TPessoa> where TPessoa : Pessoa, new()
{
    TPessoa Create(TPessoa pessoa);

    TPessoa Get(int id);

    IEnumerable<TPessoa> GetAll();

    TPessoa Update(TPessoa pessoa);

    void Delete(TPessoa pessoa);
}