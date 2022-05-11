using System.Text;
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
        using var command = commandFactory.Create("INSERT INTO Pessoas_Telefones(Numero, Observacoes) OUTPUT INSERTED.Id VALUES (@Numero, @Observacoes)");
        command.AddParameter("@Numero", telefone.Numero);
        command.AddParameter("@Observacoes", telefone.Observacoes);

        telefone.Id = (int)command.ExecuteScalar()!;

        return telefone;
    }

    public IEnumerable<Telefone> GetFrom(Pessoa pessoa)
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas_Telefones WHERE Id_Pessoa=@Id_Pessoa");
        command.AddParameter("@Id_Pessoa", pessoa.Id);
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            yield return new Telefone
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
        DeleteAllFrom(pessoa);
        CreateAllFrom(pessoa);
    }

    private void DeleteAllFrom(Pessoa pessoa)
    {
        using var command = commandFactory.Create("DELETE FROM Pessoas_Telefones WHERE Id_Pessoa=@Id_Pessoa");
        command.AddParameter("@Id_Pessoa", pessoa.Id);
        command.ExecuteNonQuery();
    }

    private void CreateAllFrom(Pessoa pessoa)
    {
        if (pessoa.Telefones is null)
            return;

        var sb = new StringBuilder();
        sb.Append("INSERT INTO Pessoas_Telefones(Id_Pessoa, Numero, Observacoes) VALUES ");

        foreach (var telefone in pessoa.Telefones)
        {
            var idPessoaToken = $"@Id_Pessoa_{telefone.Id}";
            var numeroToken = $"@Numero_{telefone.Id}";
            var observacoesToken = $"@Observacoes_{telefone.Id}";

            sb.AppendLine($"({idPessoaToken}, {numeroToken}, {observacoesToken}),");
        }

        sb.Remove(sb.Length - 1, 1);

        using var command = commandFactory.Create(sb.ToString());

        foreach (var telefone in pessoa.Telefones)
        {
            command.AddParameter($"@Id_Pessoa_{telefone.Id}", pessoa.Id);
            command.AddParameter($"@Numero_{telefone.Id}", telefone.Numero);
            command.AddParameter($"@Observacoes_{telefone.Id}", telefone.Observacoes);
        }

        command.ExecuteNonQuery();
    }
}
