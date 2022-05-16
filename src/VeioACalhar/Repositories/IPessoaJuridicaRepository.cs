using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IPessoaJuridicaRepository<TPessoa> where TPessoa : PessoaJuridica, new()
{
    TPessoa Create(TPessoa pessoa);

    TPessoa Get(int id);

    IEnumerable<TPessoa> GetAll();

    TPessoa Update(TPessoa pessoa);

    void Delete(TPessoa pessoa);
}
