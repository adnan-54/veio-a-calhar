using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IPessoaRepository
{
    Pessoa Create(Pessoa pessoa);

    Pessoa Get(int id);

    IEnumerable<Pessoa> Get();

    Pessoa Update(Pessoa pessoa);

    void Delete(Pessoa pessoa);
}