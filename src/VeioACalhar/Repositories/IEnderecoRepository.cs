using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IEnderecoRepository
{
    public Endereco Create(Endereco endereco);

    public void Delete(Endereco endereco);

    public IEnumerable<Endereco> GetFrom(Pessoa pessoa);

    void UpdateFrom(Pessoa pessoa);

    void DeleteFrom(Pessoa pessoa);

    void CreateFrom(Pessoa pessoa);
}