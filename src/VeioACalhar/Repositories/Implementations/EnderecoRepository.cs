using System.Text;
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

    public Endereco Create(Endereco endereco)
    {
        if (endereco.Pessoa?.Id is null)
            throw new Exception("Pessoa ou id da pessoa não informados");

        using var command = commandFactory.Create("INSERT INTO Pessoas_Enderecos(Id_Pessoa, Logradouro, Numero, Bairro, Cidade, Estado, CEP, Observacoes) OUTPUT INSERTED.Id VALUES (@Id_Pessoa, @Logradouro, @Numero, @Bairro, @Cidade, @Estado, @CEP, @Observacoes)");
        command.AddParameter("@Id_Pessoa", endereco.Pessoa.Id);
        command.AddParameter("@Logradouro", endereco.Logradouro);
        command.AddParameter("@Numero", endereco.Numero);
        command.AddParameter("@Bairro", endereco.Bairro);
        command.AddParameter("@Cidade", endereco.Cidade);
        command.AddParameter("@Estado", endereco.Estado);
        command.AddParameter("@CEP", endereco.CEP);
        command.AddParameter("@Observacoes", endereco.Observacoes);

        endereco.Id = (int)command.ExecuteScalar()!;

        return endereco;
    }

    public void Delete(Endereco endereco)
    {
        using var command = commandFactory.Create("DELETE FROM Pessoas_Enderecos WHERE Id = @Id");
        command.AddParameter("@Id", endereco.Id);
        command.ExecuteNonQuery();
    }

    public IEnumerable<Endereco> GetFrom(Pessoa pessoa)
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas_Enderecos WHERE Id_Pessoa = @Id_Pessoa");
        command.AddParameter("@Id_Pessoa", pessoa.Id);
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            yield return new Endereco
            {
                Id = (int)reader["Id"],
                Logradouro = (string)reader["Logradouro"],
                Numero = (int)reader["Numero"],
                Bairro = (string)reader["Bairro"],
                Cidade = (string)reader["Cidade"],
                Estado = (string)reader["Estado"],
                CEP = (string)reader["CEP"],
                Observacoes = (string)reader["Observacao"]
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
        using var command = commandFactory.Create("DELETE FROM Pessoas_Enderecos WHERE Id_Pessoa = @Id_Pessoa");
        command.AddParameter("@Id_Pessoa", pessoa.Id);
        command.ExecuteNonQuery();
    }

    public void CreateFrom(Pessoa pessoa)
    {
        if (pessoa.Enderecos is null)
            return;

        var sb = new StringBuilder();
        sb.Append("INSERT INTO Pessoas_Enderecos(Id_Pessoa, Logradouro, Numero, Bairro, Cidade, Estado, CEP, Observacoes) VALUES ");

        foreach (var endereco in pessoa.Enderecos)
        {
            var idPessoaToken = $"@Id_Pessoa_{endereco.Id}";
            var logradouroToken = $"@Logradouro_{endereco.Id}";
            var numeroToken = $"@Numero_{endereco.Id}";
            var bairroToken = $"@Bairro_{endereco.Id}";
            var cidadeToken = $"@Cidade_{endereco.Id}";
            var estadoToken = $"@Estado_{endereco.Id}";
            var cepToken = $"@CEP_{endereco.Id}";
            var observacoesToken = $"@Observacoes_{endereco.Id}";

            sb.Append($"({idPessoaToken}, {logradouroToken}, {numeroToken}, {bairroToken}, {cidadeToken}, {estadoToken}, {cepToken}, {observacoesToken}),");
        }

        sb.Remove(sb.Length - 1, 1);

        using var command = commandFactory.Create(sb.ToString());

        foreach (var endereco in pessoa.Enderecos)
        {
            command.AddParameter($"@Id_Pessoa_{endereco.Id}", pessoa.Id);
            command.AddParameter($"@Logradouro_{endereco.Id}", endereco.Logradouro);
            command.AddParameter($"@Numero_{endereco.Id}", endereco.Numero);
            command.AddParameter($"@Bairro_{endereco.Id}", endereco.Bairro);
            command.AddParameter($"@Cidade_{endereco.Id}", endereco.Cidade);
            command.AddParameter($"@Estado_{endereco.Id}", endereco.Estado);
            command.AddParameter($"@CEP_{endereco.Id}", endereco.CEP);
            command.AddParameter($"@Observacoes_{endereco.Id}", endereco.Observacoes);
        }

        command.ExecuteNonQuery();
    }
}
