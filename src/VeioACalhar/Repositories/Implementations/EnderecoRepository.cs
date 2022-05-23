using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class EnderecoRepository : IEnderecoRepository
{
    private readonly ISqlCommandFactory commandFactory;

    public EnderecoRepository(ISqlCommandFactory commandFactory)
    {
        this.commandFactory = commandFactory;
    }

    public IReadOnlyCollection<Endereco> CreateFor(Pessoa pessoa)
    {
        var enderecos = new List<Endereco>();
        foreach (var endereco in pessoa.Enderecos)
            enderecos.Add(Create(endereco, pessoa));

        return enderecos;
    }

    public IReadOnlyCollection<Endereco> GetFor(Pessoa pessoa)
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas_Enderecos WHERE Id_Pessoa = @Id_Pessoa");
        command.AddParameter("@Id_Pessoa", pessoa.Id);

        using var reader = command.ExecuteReader();

        var enderecos = new List<Endereco>();
        while (reader.Read())
            enderecos.Add(CreateEndereco(reader));

        return enderecos;
    }

    public IReadOnlyCollection<Endereco> UpdateFor(Pessoa pessoa)
    {
        DeleteFor(pessoa);
        return CreateFor(pessoa);
    }

    public void DeleteFor(Pessoa pessoa)
    {
        using var command = commandFactory.Create("DELETE FROM Pessoas_Enderecos WHERE Id_Pessoa = @Id_Pessoa");
        command.AddParameter("@Id_Pessoa", pessoa.Id);
        command.ExecuteNonQuery();
    }

    private Endereco Create(Endereco endereco, Pessoa pessoa)
    {
        using var command = commandFactory.Create("INSERT INTO Pessoas_Enderecos(Id_Pessoa, Logradouro, Numero, Bairro, Cidade, Estado, Cep, Observacoes) OUTPUT INSERTED.Id VALUES (@Id_Pessoa, @Logradouro, @Numero, @Bairro, @Cidade, @Estado, @Cep, @Observacoes)");
        command.AddParameter("@Id_Pessoa", pessoa.Id);
        command.AddParameter("@Logradouro", endereco.Logradouro);
        command.AddParameter("@Numero", endereco.Numero);
        command.AddParameter("@Bairro", endereco.Bairro);
        command.AddParameter("@Cidade", endereco.Cidade);
        command.AddParameter("@Estado", endereco.Estado);
        command.AddParameter("@Cep", endereco.Cep);
        command.AddParameter("@Observacoes", endereco.Observacoes);

        var id = command.ExecuteScalar<int>();

        return endereco with { Id = id };
    }

    private static Endereco CreateEndereco(SqlDataReader reader)
    {
        return new()
        {
            Id = (int)reader["Id"],
            Logradouro = (string)reader["Logradouro"],
            Numero = (int)reader["Numero"],
            Bairro = (string)reader["Bairro"],
            Cidade = (string)reader["Cidade"],
            Estado = (string)reader["Estado"],
            Cep = (string)reader["Cep"],
            Observacoes = (string)reader["Observacoes"],
        };
    }
}
