using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IEnderecoRepository
{
    IEnumerable<Endereco> CreateFor(Pessoa pessoa);

    IEnumerable<Endereco> GetFor(Pessoa pessoa);

    IEnumerable<Endereco> UpdateFor(Pessoa pessoa);

    void DeleteFor(Pessoa pessoa);
}
