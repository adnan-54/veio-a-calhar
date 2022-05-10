using VeioACalhar.Data;
using VeioACalhar.Models;

namespace VeioACalhar.Services;

public class CargoRepository : GenericRepository<Cargo>, ICargoRepository
{
    public CargoRepository(IDbConnection dbConnection) : base(dbConnection)
    {
    }
}

public interface ICargoRepository : IGenericRepository<Cargo>
{

}
