using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface ITelefoneRepository
{
    IEnumerable<Telefone> CreateFor(Pessoa pessoa);

    IEnumerable<Telefone> GetFor(Pessoa pessoa);

    IEnumerable<Telefone> UpdateFor(Pessoa pessoa);

    void DeleteFor(Pessoa pessoa);
}
