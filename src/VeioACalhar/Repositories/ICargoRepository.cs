using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface ICargoRepository
{
    Cargo Create(Cargo cargo);

    Cargo? Get(int id);

    IEnumerable<Cargo> Get();

    public void Update(Cargo cargo);

    public void Delete(Cargo cargo);
}
