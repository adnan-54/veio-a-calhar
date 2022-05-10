using System.Data.SqlClient;
using VeioACalhar.Data;
using VeioACalhar.Models;

namespace VeioACalhar.Services;

public class CargoRepository : ICargoRepository
{
    private readonly IDbConnection dbConnection;
    private readonly Dictionary<string, Func<Cargo, object?>> parameters;

    public CargoRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
        parameters = new();

        parameters.Add("id", c => c.Id);
        parameters.Add("nome", c => c.Nome);
    }

    public void Create(Cargo model)
    {
        var command = new SqlCommand("INSERT INTO Cargos VALUES (@nome)");
        command.Parameters.AddWithValue("@nome", model.Nome);
        dbConnection.ExecuteNonQuery(command);
    }

    public Cargo Get(int id)
    {
        var command = new SqlCommand("SELECT * FROM Cargos WHERE Id = @id");
        command.Parameters.AddWithValue("@id", id);

        var reader = dbConnection.ExecuteReader(command);

        if (!reader.HasRows)
            throw new Exception($"Nenhum cargo encontrado com o Id {id}");
        if (!reader.Read())
            throw new Exception($"Ocorreu um erro ao tentar ler o cargo com Id {id}");

        return CreateCargo(reader);
    }

    public IEnumerable<Cargo> Get()
    {
        var command = new SqlCommand("SELECT * FROM Cargos");
        var reader = dbConnection.ExecuteReader(command);

        while (reader.Read())
            yield return CreateCargo(reader);
    }

    public void Update(Cargo cargo)
    {
        var command = new SqlCommand("UPDATE Cargos SET Nome = @nome WHERE Id = @id");
        command.Parameters.AddWithValue("@id", cargo.Id);
        command.Parameters.AddWithValue("@nome", cargo.Nome);

        dbConnection.ExecuteNonQuery(command);
    }

    public void Delete(int id)
    {
        var command = new SqlCommand("DELETE FROM Cargos WHERE Id = @id");
        command.Parameters.AddWithValue("@id", id);
    }

    private static Cargo CreateCargo(SqlDataReader reader)
    {
        return new Cargo()
        {
            Id = (int)reader["Id"],
            Nome = (string)reader["Nome"]
        };
    }
}

public interface ICargoRepository
{
    void Create(Cargo model);

    Cargo Get(int id);

    IEnumerable<Cargo> Get();

    void Update(Cargo cargo);

    void Delete(int id);
}