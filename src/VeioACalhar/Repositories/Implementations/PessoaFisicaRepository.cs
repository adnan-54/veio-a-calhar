using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class PessoaFisicaRepository : IPessoaFisicaRepository
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly IPessoaRepository pessoaRepository;

    public PessoaFisicaRepository(ISqlCommandFactory commandFactory, IPessoaRepository pessoaRepository)
    {
        this.commandFactory = commandFactory;
        this.pessoaRepository = pessoaRepository;
    }

    public PessoaFisica Create(PessoaFisica pessoaFisica)
    {
        var pessoa = pessoaRepository.Create(pessoaFisica);
        using var command = commandFactory.Create("INSERT INTO Pessoas_Fisicas(Id, CPF, RG) VALUES (@Id, @CPF, @RG)");
        command.AddParameter("@Id", pessoa.Id);
        command.AddParameter("@CPF", pessoaFisica.Cpf);
        command.AddParameter("@RG", pessoaFisica.Rg);
        command.ExecuteNonQuery();

        return pessoaFisica with { Id = pessoa.Id };
    }

    public PessoaFisica Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas_Fisicas WHERE Id = @Id");
        command.AddParameter("@Id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
            return CreatePessoaFisica(reader);
        return new();
    }

    public IEnumerable<PessoaFisica> Get()
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas_Fisicas");
        using var reader = command.ExecuteReader();

        while (reader.Read())
            yield return CreatePessoaFisica(reader);
    }

    public PessoaFisica Update(PessoaFisica pessoaFisica)
    {
        using var command = commandFactory.Create("UPDATE Pessoas_Fisicas SET CPF = @CPF, RG = @RG WHERE Id = @Id");
        command.AddParameter("@Id", pessoaFisica.Id);
        command.AddParameter("@CPF", pessoaFisica.Cpf);
        command.AddParameter("@RG", pessoaFisica.Rg);
        command.ExecuteNonQuery();

        return (PessoaFisica)pessoaRepository.Update(pessoaFisica);
    }

    public void Delete(PessoaFisica pessoaFisica)
    {
        using var command = commandFactory.Create("DELETE FROM Pessoas_Fisicas WHERE Id = @Id");
        command.AddParameter("@Id", pessoaFisica.Id);
        command.ExecuteNonQuery();

        pessoaRepository.Delete(pessoaFisica);
    }

    private PessoaFisica CreatePessoaFisica(SqlDataReader reader)
    {
        var id = (int)reader["Id"];
        var pessoa = pessoaRepository.Get(id);

        return new()
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Observacoes = pessoa.Observacoes,
            Pix = pessoa.Pix,
            Email = pessoa.Email,
            Enderecos = pessoa.Enderecos,
            Telefones = pessoa.Telefones,
            Cpf = (string)reader["CPF"],
            Rg = (string)reader["RG"]
        };
    }
}
