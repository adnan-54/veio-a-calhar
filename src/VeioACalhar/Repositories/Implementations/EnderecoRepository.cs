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

    public IEnumerable<Endereco> CreateFrom(Pessoa pessoa)
    {
        foreach (var endereco in pessoa.Enderecos)
            yield return Create(endereco, pessoa);
    }

    public IEnumerable<Endereco> GetFrom(Pessoa pessoa)
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas_Enderecos WHERE Id_Pessoa = @Id_Pessoa");
        command.AddParameter("@Id_Pessoa", pessoa.Id);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            yield return new()
            {
                Id = (int)reader["Id"],
                Logradouro = (string)reader["Logradouro"],
                Numero = (int)reader["Numero"],
                Bairro = (string)reader["Bairro"],
                Cidade = (string)reader["Cidade"],
                Estado = (string)reader["Estado"],
                Cep = (string)reader["CEP"],
                Observacoes = (string)reader["Observacao"]
            };
        }
    }

    public IEnumerable<Endereco> UpdateFrom(Pessoa pessoa)
    {
        DeleteFrom(pessoa);
        return CreateFrom(pessoa);
    }

    public void DeleteFrom(Pessoa pessoa)
    {
        using var command = commandFactory.Create("DELETE FROM Pessoas_Enderecos WHERE Id_Pessoa = @Id_Pessoa");
        command.AddParameter("@Id_Pessoa", pessoa.Id);
        command.ExecuteNonQuery();
    }

    private Endereco Create(Endereco endereco, Pessoa pessoa)
    {
        using var command = commandFactory.Create("INSERT INTO Pessoas_Enderecos(Id_Pessoa, Logradouro, Numero, Bairro, Cidade, Estado, CEP, Observacoes) OUTPUT INSERTED.Id VALUES (@Id_Pessoa, @Logradouro, @Numero, @Bairro, @Cidade, @Estado, @CEP, @Observacoes)");
        command.AddParameter("@Id_Pessoa", pessoa.Id);
        command.AddParameter("@Logradouro", endereco.Logradouro);
        command.AddParameter("@Numero", endereco.Numero);
        command.AddParameter("@Bairro", endereco.Bairro);
        command.AddParameter("@Cidade", endereco.Cidade);
        command.AddParameter("@Estado", endereco.Estado);
        command.AddParameter("@CEP", endereco.Cep);
        command.AddParameter("@Observacoes", endereco.Observacoes);

        var id = (int)command.ExecuteScalar()!;

        return endereco with { Id = id, Pessoa = pessoa };
    }
}
