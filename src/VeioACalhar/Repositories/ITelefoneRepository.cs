using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface ITelefoneRepository
{
    IEnumerable<Telefone> CreateFrom(Pessoa pessoa);

    IEnumerable<Telefone> GetFrom(Pessoa pessoa);

    IEnumerable<Telefone> UpdateFrom(Pessoa pessoa);

    void DeleteFrom(Pessoa pessoa);
}
