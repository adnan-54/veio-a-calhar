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
        command.AddParameter("@CPF", pessoaFisica.CPF);
        command.AddParameter("@RG", pessoaFisica.RG);

        pessoaFisica.Id = pessoa.Id;
        pessoaFisica.Nome = pessoa.Nome;
        pessoaFisica.Observacoes = pessoa.Observacoes;
        pessoaFisica.PIX = pessoa.PIX;
        pessoaFisica.Email = pessoa.Email;
        pessoaFisica.Enderecos = pessoa.Enderecos;
        pessoaFisica.Telefones = pessoa.Telefones;

        return pessoaFisica;
    }

    public PessoaFisica? Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas_Fisicas WHERE Id = @Id");
        command.AddParameter("@Id", id);
        var reader = command.ExecuteReader();
        
        if (!reader.Read())
            return null;

        var pessoa = pessoaRepository.Get(id)!;

        return CreatePessoaFisica(pessoa, reader);
    }

    public IEnumerable<PessoaFisica> Get()
    {
        var pessoas = pessoaRepository.Get();
        foreach (var pessoa in pessoas)
        {
            using var command = commandFactory.Create("SELECT * FROM Pessoas_Fisicas WHERE Id = @Id");
            command.AddParameter("@Id", pessoa.Id);
            var reader = command.ExecuteReader();
            
            if (!reader.Read())
                continue;

            yield return CreatePessoaFisica(pessoa, reader);
        }
    }

    public void Update(PessoaFisica pessoaFisica)
    {
        pessoaRepository.Update(pessoaFisica);
        using var command = commandFactory.Create("UPDATE Pessoas_Fisicas SET CPF = @CPF, RG = @RG WHERE Id = @Id");
        command.AddParameter("@Id", pessoaFisica.Id);
        command.AddParameter("@CPF", pessoaFisica.CPF);
        command.AddParameter("@RG", pessoaFisica.RG);
        command.ExecuteNonQuery();
    }

    public void Delete(PessoaFisica pessoaFisica)
    {
        pessoaRepository.Delete(pessoaFisica);
        using var command = commandFactory.Create("DELETE FROM Pessoas_Fisicas WHERE Id = @Id");
        command.AddParameter("@Id", pessoaFisica.Id);
        command.ExecuteNonQuery();
    }

    private static PessoaFisica CreatePessoaFisica(Pessoa pessoa, SqlDataReader reader)
    {
        return new()
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Observacoes = pessoa.Observacoes,
            PIX = pessoa.PIX,
            Email = pessoa.Email,
            Enderecos = pessoa.Enderecos,
            Telefones = pessoa.Telefones,
            CPF = (string)reader["CPF"],
            RG = (string)reader["RG"]
        };
    }
}
