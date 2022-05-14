using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IEnderecoRepository
{
    IEnumerable<Endereco> CreateFrom(Pessoa pessoa);

    IEnumerable<Endereco> GetFrom(Pessoa pessoa);

    IEnumerable<Endereco> UpdateFrom(Pessoa pessoa);

    void DeleteFrom(Pessoa pessoa);
}