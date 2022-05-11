using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IEnderecoRepository
{
    public Endereco Create(Endereco endereco);

    public IEnumerable<Endereco> GetFor(Pessoa pessoa);

    void UpdateFrom(Pessoa pessoa);
}