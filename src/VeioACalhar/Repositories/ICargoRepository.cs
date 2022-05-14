using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface ICargoRepository
{
    Cargo Create(Cargo cargo);

    Cargo Get(int id);

    IEnumerable<Cargo> Get();

    Cargo Update(Cargo cargo);

    void Delete(Cargo cargo);
}
