using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface ITelefoneRepository
{
    public Telefone Create(Telefone telefone);

    public void Delete(Telefone telefone);

    public IEnumerable<Telefone> GetFrom(Pessoa pessoa);

    void UpdateFrom(Pessoa pessoa);

    void DeleteFrom(Pessoa pessoa);

    void CreateFrom(Pessoa pessoa);
}
