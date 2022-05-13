using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IPessoaRepository
{
    Pessoa Create(Pessoa pessoa);

    Pessoa Get(int id);

    IEnumerable<Pessoa> Get();

    public void Update(Pessoa pessoa);

    public void Delete(Pessoa pessoa);
}