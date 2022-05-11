using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface ITelefoneRepository
{
    public Telefone Create(Telefone telefone);

    public IEnumerable<Telefone> GetFrom(Pessoa pessoa);

    void UpdateFrom(Pessoa pessoa);
}
