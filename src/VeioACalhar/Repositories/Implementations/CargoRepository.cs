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

        var id = command.ExecuteScalar<int>();

        return cargo with { Id = id };
    }

    public Cargo Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Cargos WHERE Id = @Id");
        command.AddParameter("@Id", id);
        using var reader = command.ExecuteReader();

        if (reader.Read())
            return CreateCargo(reader);
        return new();
    }

    public IReadOnlyCollection<Cargo> GetAll()
    {
        using var command = commandFactory.Create("SELECT * FROM Cargos");
        using var reader = command.ExecuteReader();

        var cargos = new List<Cargo>();
        while (reader.Read())
            cargos.Add(CreateCargo(reader));
        return cargos;
    }

    public Cargo Update(Cargo cargo)
    {
        using var command = commandFactory.Create("UPDATE Cargos SET Nome = @Nome WHERE Id = @Id");
        command.AddParameter("@Id", cargo.Id);
        command.AddParameter("@Nome", cargo.Nome);
        command.ExecuteNonQuery();

        return cargo;
    }

    public void Delete(Cargo cargo)
    {
        using var command = commandFactory.Create("DELETE FROM Cargos WHERE Id = @Id");
        command.AddParameter("@Id", cargo.Id);
        command.ExecuteNonQuery();
    }

    private static Cargo CreateCargo(SqlDataReader reader)
    {
        return new Cargo
        {
            Id = reader.GetInt32(0),
            Nome = reader.GetString(1)
        };
    }
}
