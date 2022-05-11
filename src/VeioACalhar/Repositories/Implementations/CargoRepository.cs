using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class CargoRepository : ICargoRepository
{
    private readonly ISqlCommandFactory commandFactory;

    public CargoRepository(ISqlCommandFactory commandFactory)
    {
        this.commandFactory = commandFactory;
    }

    public Cargo Create(Cargo cargo)
    {
        using var command = commandFactory.Create("INSERT INTO Cargos(Nome) OUTPUT INSERTED.Id VALUES (@Nome)");
        command.AddParameter("@Nome", cargo.Nome);

        cargo.Id = (int)command.ExecuteScalar()!;

        return cargo;
    }

    public Cargo? Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Cargos WHERE Id=@Id");
        command.AddParameter("@Id", id);
        var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Cargo()
            {
                Id = (int)reader["Id"],
                Nome = (string)reader["Nome"]
            };
        }

        return null;
    }

    public IEnumerable<Cargo> Get()
    {
        using var command = commandFactory.Create("SELECT * FROM Cargos");
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            yield return new Cargo()
            {
                Id = (int)reader["Id"],
                Nome = (string)reader["Nome"]
            };
        }
    }

    public void Update(Cargo cargo)
    {
        using var command = commandFactory.Create("UPDATE Cargos SET (Nome=@Nome) WHERE Id=@Id");
        command.AddParameter("@Id", cargo.Id);
        command.AddParameter("@Nome", cargo.Nome);

        command.ExecuteNonQuery();
    }

    public void Delete(Cargo cargo)
    {
        using var command = commandFactory.Create("DELETE FROM Cargos WHERE Id=@Id");
        command.AddParameter("@Id", cargo.Id);

        command.ExecuteNonQuery();
    }
}
