using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class PessoaRepository<TPessoa> : IPessoaRepository<TPessoa> where TPessoa : Pessoa, new()
{
    private readonly ISqlCommandFactory commandFactory;

    public PessoaRepository(ISqlCommandFactory commandFactory)
    {
        this.commandFactory = commandFactory;
    }

    public TPessoa Create(TPessoa pessoa)
    {
        using var command = commandFactory.Create("INSERT INTO Pessoas(Nome, Observacoes, Pix, Email, Telefone, Endereco) OUTPUT INSERTED.Id VALUES(@Nome, @Observacoes, @Pix, @Email, @Telefone, @Endereco)");
        command.AddParameter("@Nome", pessoa.Nome);
        command.AddParameter("@Observacoes", pessoa.Observacoes);
        command.AddParameter("@Pix", pessoa.Pix);
        command.AddParameter("@Email", pessoa.Email);
        command.AddParameter("@Telefone", pessoa.Telefone);
        command.AddParameter("@Endereco", pessoa.Endereco);

        var id = command.ExecuteScalar<int>();

        return pessoa with { Id = id };
    }

    public TPessoa Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas WHERE Id = @Id");
        command.AddParameter("@Id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
            return CreatePessoa(reader);
        return new();
    }

    public IReadOnlyCollection<TPessoa> GetAll()
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas");
        using var reader = command.ExecuteReader();

        var pessoas = new List<TPessoa>();
        while (reader.Read())
            pessoas.Add(CreatePessoa(reader));

        return pessoas;
    }

    public TPessoa Update(TPessoa pessoa)
    {
        using var command = commandFactory.Create("UPDATE Pessoas SET Nome = @Nome, Observacoes = @Observacoes, Pix = @Pix, Email = @Email, Telefone = @Telefone, Endereco = @Endereco WHERE Id = @Id");
        command.AddParameter("@Id", pessoa.Id);
        command.AddParameter("@Nome", pessoa.Nome);
        command.AddParameter("@Observacoes", pessoa.Observacoes);
        command.AddParameter("@Pix", pessoa.Pix);
        command.AddParameter("@Email", pessoa.Email);
        command.AddParameter("@Telefone", pessoa.Telefone);
        command.AddParameter("@Endereco", pessoa.Endereco);

        command.ExecuteNonQuery();

        return pessoa;
    }

    public void Delete(TPessoa pessoa)
    {
        using var command = commandFactory.Create("DELETE FROM Pessoas WHERE Id = @Id");
        command.AddParameter("@Id", pessoa.Id);
        command.ExecuteNonQuery();
    }

    private static TPessoa CreatePessoa(SqlDataReader reader)
    {
        return new TPessoa()
        {
            Id = reader.GetInt32(0),
            Nome = reader.GetString(1),
            Observacoes = reader.GetString(2),
            Pix = reader.GetString(3),
            Email = reader.GetString(4),
            Telefone = reader.GetString(5),
            Endereco = reader.GetString(6)
        };
    }
}
