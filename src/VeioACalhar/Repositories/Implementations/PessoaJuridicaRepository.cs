using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class PessoaJuridicaRepository : IPessoaJuridicaRepository
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly IPessoaRepository pessoaRepository;

    public PessoaJuridicaRepository(ISqlCommandFactory commandFactory, IPessoaRepository pessoaRepository)
    {
        this.commandFactory = commandFactory;
        this.pessoaRepository = pessoaRepository;
    }

    public PessoaJuridica Create(PessoaJuridica pessoaJuridica)
    {
        var pessoa = pessoaRepository.Create(pessoaJuridica);
        using var command = commandFactory.Create("INSERT INTO Pessoas_Juridicas(Id, CNPJ, Inscricao_Estadual) VALUES (@Id, @CNPJ, @Inscricao_Estadual)");
        command.AddParameter("@Id", pessoa.Id);
        command.AddParameter("@CNPJ", pessoaJuridica.CNPJ);
        command.AddParameter("@Inscricao_Estadual", pessoaJuridica.InscricaoEstadual);

        pessoaJuridica.Id = pessoa.Id;
        pessoaJuridica.Nome = pessoa.Nome;
        pessoaJuridica.Observacoes = pessoa.Observacoes;
        pessoaJuridica.PIX = pessoa.PIX;
        pessoaJuridica.Email = pessoa.Email;
        pessoaJuridica.Enderecos = pessoa.Enderecos;
        pessoaJuridica.Telefones = pessoa.Telefones;

        return pessoaJuridica;
    }

    public PessoaJuridica? Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas_Juridicas WHERE Id = @Id");
        command.AddParameter("@Id", id);
        var reader = command.ExecuteReader();

        if (!reader.Read())
            return null;

        var pessoa = pessoaRepository.Get(id)!;

        return CreatePessoaJuridica(pessoa, reader);
    }

    public IEnumerable<PessoaJuridica> Get()
    {
        var pessoas = pessoaRepository.Get();
        foreach (var pessoa in pessoas)
        {
            using var command = commandFactory.Create("SELECT * FROM Pessoas_Juridicas WHERE Id = @Id");
            command.AddParameter("@Id", pessoa.Id);
            var reader = command.ExecuteReader();

            if (!reader.Read())
                continue;

            yield return CreatePessoaJuridica(pessoa, reader);
        }
    }

    public void Update(PessoaJuridica pessoaJuridica)
    {
        pessoaRepository.Update(pessoaJuridica);
        using var command = commandFactory.Create("UPDATE Pessoas_Juridicas SET CNPJ = @CNPJ, Inscricao_Estadual = @Inscricao_Estadual WHERE Id = @Id");
        command.AddParameter("@Id", pessoaJuridica.Id);
        command.AddParameter("@CNPJ", pessoaJuridica.CNPJ);
        command.AddParameter("@Inscricao_Estadual", pessoaJuridica.InscricaoEstadual);
        command.ExecuteNonQuery();
    }

    public void Delete(PessoaJuridica pessoaJuridica)
    {
        pessoaRepository.Delete(pessoaJuridica);
        using var command = commandFactory.Create("DELETE FROM Pessoas_Juridicas WHERE Id = @Id");
        command.AddParameter("@Id", pessoaJuridica.Id);
        command.ExecuteNonQuery();
    }

    private static PessoaJuridica CreatePessoaJuridica(Pessoa pessoa, SqlDataReader reader)
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
            CNPJ = (string)reader["CNPJ"],
            InscricaoEstadual = (string)reader["Inscricao_Estadual"]
        };
    }
}
