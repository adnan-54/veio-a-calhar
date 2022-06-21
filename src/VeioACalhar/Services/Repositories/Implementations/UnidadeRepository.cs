using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class UnidadeRepository : IUnidadeRepository
{
    private readonly ISqlCommandFactory commandFactory;

    public UnidadeRepository(ISqlCommandFactory commandFactory)
    {
        this.commandFactory = commandFactory;
    }

    public Unidade Create(Unidade unidade)
    {
        using var command = commandFactory.Create("INSERT INTO Unidades(Nome, Sigla) OUTPUT INSERTED.Id VALUES (@Nome, @Sigla)");
        command.AddParameter("@Nome", unidade.Nome);
        command.AddParameter("@Sigla", unidade.Sigla);

        var id = command.ExecuteScalar<int>();

        return unidade with { Id = id };
    }

    public Unidade Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Unidades WHERE Id = @Id");
        command.AddParameter("@Id", id);
        using var reader = command.ExecuteReader();

        if (reader.Read())
            return CreateUnidade(reader);
        return new();
    }

    public IReadOnlyCollection<Unidade> GetAll()
    {
        using var command = commandFactory.Create("SELECT * FROM Unidades");
        using var reader = command.ExecuteReader();

        var unidades = new List<Unidade>();
        while (reader.Read())
            unidades.Add(CreateUnidade(reader));
        return unidades;
    }

    public Unidade Update(Unidade unidade)
    {
        using var command = commandFactory.Create("UPDATE Unidades SET Nome = @Nome, Sigla = @Sigla WHERE Id = @Id");
        command.AddParameter("@Id", unidade.Id);
        command.AddParameter("@Nome", unidade.Nome);
        command.AddParameter("@Sigla", unidade.Sigla);
        command.ExecuteNonQuery();

        return unidade;
    }

    public void Delete(Unidade unidade)
    {
        using var command = commandFactory.Create("DELETE FROM Unidades WHERE Id = @Id");
        command.AddParameter("@Id", unidade.Id);
        command.ExecuteNonQuery();
    }

    private static Unidade CreateUnidade(SqlDataReader reader)
    {
        return new Unidade
        {
            Id = reader.GetInt32(0),
            Nome = reader.GetString(1),
            Sigla = reader.GetString(2)
        };
    }
}
