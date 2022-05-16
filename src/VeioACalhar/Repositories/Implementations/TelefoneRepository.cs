using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class TelefoneRepository : ITelefoneRepository
{
    private readonly ISqlCommandFactory commandFactory;

    public TelefoneRepository(ISqlCommandFactory commandFactory)
    {
        this.commandFactory = commandFactory;
    }

    public IEnumerable<Telefone> CreateFor(Pessoa pessoa)
    {
        foreach (var telefone in pessoa.Telefones)
            yield return Create(telefone, pessoa);
    }

    public IEnumerable<Telefone> GetFor(Pessoa pessoa)
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas_Telefones WHERE Id_Pessoa = @Id_Pessoa");
        command.AddParameter("@Id_Pessoa", pessoa.Id);

        using var reader = command.ExecuteReader();

        while (reader.Read())
            yield return CreateTelefone(reader, pessoa);
    }

    public IEnumerable<Telefone> UpdateFor(Pessoa pessoa)
    {
        DeleteFor(pessoa);
        return CreateFor(pessoa);
    }

    public void DeleteFor(Pessoa pessoa)
    {
        using var command = commandFactory.Create("DELETE FROM Pessoas_Telefones WHERE Id_Pessoa = @Id_Pessoa");
        command.AddParameter("@Id_Pessoa", pessoa.Id);
        command.ExecuteNonQuery();
    }

    private Telefone Create(Telefone telefone, Pessoa pessoa)
    {
        using var command = commandFactory.Create("INSERT INTO Pessoas_Telefones(Id_Pessoa, Numero, Observacoes) OUTPUT INSERTED.Id VALUES (@Id_Pessoa, @Numero, @Observacoes)");
        command.AddParameter("@Id_Pessoa", pessoa.Id);
        command.AddParameter("@Numero", telefone.Numero);
        command.AddParameter("@Observacoes", telefone.Observacoes);

        var id = command.ExecuteNonQuery();

        return telefone with { Id = id, Pessoa = pessoa };
    }

    private static Telefone CreateTelefone(SqlDataReader reader, Pessoa pessoa)
    {
        return new()
        {
            Id = (int)reader["Id"],
            Numero = (string)reader["Numero"],
            Observacoes = (string)reader["Observacoes"],
            Pessoa = pessoa
        };
    }
}
