using System.Data.SqlClient;
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

        var id = (int)command.ExecuteScalar()!;
        return cargo with { Id = id };
    }

    public Cargo Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Cargos WHERE Id=@Id");
        command.AddParameter("@Id", id);
        using var reader = command.ExecuteReader();

        if (reader.Read())
            return CreateCargo(reader);
        return new();
    }

    public IEnumerable<Cargo> Get()
    {
        using var command = commandFactory.Create("SELECT * FROM Cargos");
        using var reader = command.ExecuteReader();

        while (reader.Read())
            yield return CreateCargo(reader);
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

    private static Cargo CreateCargo(SqlDataReader reader)
    {
        return new()
        {
            Id = (int)reader["Id"],
            Nome = (string)reader["Nome"]
        };
    }

}
