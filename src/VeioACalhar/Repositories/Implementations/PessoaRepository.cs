using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class PessoaRepository<TPessoa> : IPessoaRepository<TPessoa> where TPessoa : Pessoa, new()
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

    public TPessoa Create(TPessoa pessoa)
    {
        using var command = commandFactory.Create("INSERT INTO Pessoas(Nome, Observacoes, Pix, Email) OUTPUT INSERTED.Id VALUES(@Nome, @Observacoes, @Pix, @Email)");
        command.AddParameter("@Nome", pessoa.Nome);
        command.AddParameter("@Observacoes", pessoa.Observacoes);
        command.AddParameter("@Pix", pessoa.Pix);
        command.AddParameter("@Email", pessoa.Email);

        var id = command.ExecuteScalar<int>();

        pessoa = pessoa with { Id = id };

        var telefones = telefoneRepository.CreateFor(pessoa);
        var enderecos = enderecoRepository.CreateFor(pessoa);

        return pessoa with { Telefones = telefones, Enderecos = enderecos };
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
        using var command = commandFactory.Create("UPDATE Pessoas SET Nome = @Nome, Observacoes = @Observacoes, Pix = @Pix, Email = @Email WHERE Id = @Id");
        command.AddParameter("@Nome", pessoa.Nome);
        command.AddParameter("@Observacoes", pessoa.Observacoes);
        command.AddParameter("@Pix", pessoa.Pix);
        command.AddParameter("@Email", pessoa.Email);
        command.AddParameter("@Id", pessoa.Id);

        command.ExecuteNonQuery();

        var telefones = telefoneRepository.UpdateFor(pessoa);
        var enderecos = enderecoRepository.UpdateFor(pessoa);

        return pessoa with { Telefones = telefones, Enderecos = enderecos };
    }

    public void Delete(TPessoa pessoa)
    {
        telefoneRepository.DeleteFor(pessoa);
        enderecoRepository.DeleteFor(pessoa);

        using var command = commandFactory.Create("DELETE FROM Pessoas WHERE Id = @Id");
        command.AddParameter("@Id", pessoa.Id);

        command.ExecuteNonQuery();
    }

    private TPessoa CreatePessoa(SqlDataReader reader)
    {
        var pessoa = new TPessoa()
        {
            Id = (int)reader["Id"],
            Nome = (string)reader["Nome"],
            Observacoes = (string)reader["Observacoes"],
            Pix = (string)reader["Pix"],
            Email = (string)reader["Email"]
        };

        var telefones = telefoneRepository.GetFor(pessoa);
        var enderecos = enderecoRepository.GetFor(pessoa);

        return pessoa with { Telefones = telefones, Enderecos = enderecos };
    }
}
