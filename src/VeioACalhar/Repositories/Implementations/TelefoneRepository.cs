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

    public IReadOnlyCollection<Telefone> CreateFor(Pessoa pessoa)
    {
        var telefones = new List<Telefone>();
        foreach (var telefone in pessoa.Telefones)
            telefones.Add(Create(telefone, pessoa));
        return telefones;
    }

    public IReadOnlyCollection<Telefone> GetFor(Pessoa pessoa)
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas_Telefones WHERE Id_Pessoa = @Id_Pessoa");
        command.AddParameter("@Id_Pessoa", pessoa.Id);

        using var reader = command.ExecuteReader();

        var telefones = new List<Telefone>();

        while (reader.Read())
            telefones.Add(CreateTelefone(reader, pessoa));

        return telefones;
    }

    public IReadOnlyCollection<Telefone> UpdateFor(Pessoa pessoa)
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

        var id = command.ExecuteScalar<int>();

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
