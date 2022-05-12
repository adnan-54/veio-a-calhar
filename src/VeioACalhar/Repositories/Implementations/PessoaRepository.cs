using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly ITelefoneRepository telefoneRepository;
    private readonly IEnderecoRepository enderecoRepository;

    public PessoaRepository(ISqlCommandFactory commandFactory, ITelefoneRepository telefoneRepository, IEnderecoRepository enderecoRepository)
    {
        this.commandFactory = commandFactory;
        this.telefoneRepository = telefoneRepository;
        this.enderecoRepository = enderecoRepository;
    }

    public Pessoa Create(Pessoa pessoa)
    {
        using var command = commandFactory.Create("INSERT INTO Pessoas(Nome, Observacoes, PIX, Email) OUTPUT INSERTED.Id VALUES (@Nome, @Observacoes, @PIX, @Email)");
        command.AddParameter("@Nome", pessoa.Nome);
        command.AddParameter("@Observacoes", pessoa.Observacoes);
        command.AddParameter("@PIX", pessoa.PIX);
        command.AddParameter("@Email", pessoa.Email);

        pessoa.Id = (int)command.ExecuteScalar()!;

        telefoneRepository.CreateFrom(pessoa);
        enderecoRepository.CreateFrom(pessoa);

        return pessoa;
    }

    public Pessoa? Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas WHERE Id=@Id");
        command.AddParameter("@Id", id);
        var reader = command.ExecuteReader();

        if (reader.Read())
            return CreatePessoa(reader);
        return null;
    }

    public IEnumerable<Pessoa> Get()
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas");
        var reader = command.ExecuteReader();
        while (reader.Read())
            yield return CreatePessoa(reader);
    }

    public void Update(Pessoa pessoa)
    {
        using var command = commandFactory.Create("UPDATE Pessoas SET Nome=@Nome, Observacoes=@Observacoes, PIX=@PIX, Email=@Email WHERE Id=@Id");
        command.AddParameter("@Nome", pessoa.Nome);
        command.AddParameter("@Observacoes", pessoa.Observacoes);
        command.AddParameter("@PIX", pessoa.PIX);
        command.AddParameter("@Email", pessoa.Email);
        command.AddParameter("@Id", pessoa.Id);
        command.ExecuteNonQuery();

        telefoneRepository.UpdateFrom(pessoa);
        enderecoRepository.UpdateFrom(pessoa);
    }

    public void Delete(Pessoa pessoa)
    {
        telefoneRepository.DeleteFrom(pessoa);
        enderecoRepository.DeleteFrom(pessoa);

        using var command = commandFactory.Create("DELETE FROM Pessoas WHERE Id=@Id");
        command.AddParameter("@Id", pessoa.Id);
        command.ExecuteNonQuery();
    }

    private Pessoa CreatePessoa(SqlDataReader reader)
    {
        var pessoa = new Pessoa()
        {
            Id = (int)reader["Id"],
            Nome = (string)reader["Nome"],
            Observacoes = (string)reader["Observacoes"],
            PIX = (string)reader["PIX"],
            Email = (string)reader["Email"]
        };

        pessoa.Telefones = telefoneRepository.GetFrom(pessoa);
        pessoa.Enderecos = enderecoRepository.GetFrom(pessoa);

        return pessoa;
    }
}
