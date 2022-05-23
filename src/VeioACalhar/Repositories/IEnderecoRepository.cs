using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IEnderecoRepository
{
    IReadOnlyCollection<Endereco> CreateFor(Pessoa pessoa);

    IReadOnlyCollection<Endereco> GetFor(Pessoa pessoa);

    IReadOnlyCollection<Endereco> UpdateFor(Pessoa pessoa);

    void DeleteFor(Pessoa pessoa);
}
