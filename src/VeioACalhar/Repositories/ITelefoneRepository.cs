using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface ITelefoneRepository
{
    IReadOnlyCollection<Telefone> CreateFor(Pessoa pessoa);

    IReadOnlyCollection<Telefone> GetFor(Pessoa pessoa);

    IReadOnlyCollection<Telefone> UpdateFor(Pessoa pessoa);

    void DeleteFor(Pessoa pessoa);
}
