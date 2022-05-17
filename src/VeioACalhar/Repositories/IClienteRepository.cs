using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IClienteRepository
{
    Pessoa Create(Pessoa cliente);

    Pessoa Get(int id);

    IReadOnlyCollection<Pessoa> GetAll();

    Pessoa Update(Pessoa cliente);

    void Delete(Pessoa cliente);
}