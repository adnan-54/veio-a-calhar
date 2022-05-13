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

    public Telefone Create(Telefone telefone)
    {
        using var command = commandFactory.Create("INSERT INTO Pessoas_Telefones(Id_Pessoa, Numero, Observacoes) OUTPUT INSERTED.Id VALUES (@Id_Pessoa, @Numero, @Observacoes)");
        command.AddParameter("@Id_Pessoa", telefone.Pessoa.Id);
        command.AddParameter("@Numero", telefone.Numero);
        command.AddParameter("@Observacoes", telefone.Observacoes);

        var id = (int)command.ExecuteScalar()!;

        return telefone with { Id = id };
    }

    public void Delete(Telefone telefone)
    {
        using var command = commandFactory.Create("DELETE FROM Pessoas_Telefones WHERE Id = @Id");
        command.AddParameter("@Id", telefone.Id);
        command.ExecuteNonQuery();
    }

    public IEnumerable<Telefone> GetFrom(Pessoa pessoa)
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas_Telefones WHERE Id_Pessoa=@Id_Pessoa");
        command.AddParameter("@Id_Pessoa", pessoa.Id);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            yield return new()
            {
                Id = (int)reader["Id"],
                Pessoa = pessoa,
                Numero = (string)reader["Numero"],
                Observacoes = (string)reader["Observacoes"]
            };
        }
    }

    public void UpdateFrom(Pessoa pessoa)
    {
        DeleteFrom(pessoa);
        CreateFrom(pessoa);
    }

    public void DeleteFrom(Pessoa pessoa)
    {
        using var command = commandFactory.Create("DELETE FROM Pessoas_Telefones WHERE Id_Pessoa=@Id_Pessoa");
        command.AddParameter("@Id_Pessoa", pessoa.Id);
        command.ExecuteNonQuery();
    }

    public void CreateFrom(Pessoa pessoa)
    {
        foreach (var telefone in pessoa.Telefones)
            Create(telefone);
    }
}
