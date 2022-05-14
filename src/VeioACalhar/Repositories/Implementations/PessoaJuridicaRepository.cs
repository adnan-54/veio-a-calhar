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
        pessoaJuridica = (PessoaJuridica)pessoaRepository.Create(pessoaJuridica);
        using var command = commandFactory.Create("INSERT INTO Pessoas_Juridicas(Id, CNPJ, Inscricao_Estadual) VALUES (@Id, @CNPJ, @Inscricao_Estadual)");
        command.AddParameter("@Id", pessoaJuridica.Id);
        command.AddParameter("@CNPJ", pessoaJuridica.Cnpj);
        command.AddParameter("@Inscricao_Estadual", pessoaJuridica.InscricaoEstadual);
        command.ExecuteNonQuery();

        return pessoaJuridica;
    }

    public PessoaJuridica Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas_Juridicas WHERE Id = @Id");
        command.AddParameter("@Id", id);
        using var reader = command.ExecuteReader();

        if (reader.Read())
            return CreatePessoaJuridica(reader);
        return new();
    }

    public IEnumerable<PessoaJuridica> Get()
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas_Juridicas");
        using var reader = command.ExecuteReader();

        while (reader.Read())
            yield return CreatePessoaJuridica(reader);
    }

    public PessoaJuridica Update(PessoaJuridica pessoaJuridica)
    {
        using var command = commandFactory.Create("UPDATE Pessoas_Juridicas SET CNPJ = @CNPJ, Inscricao_Estadual = @Inscricao_Estadual WHERE Id = @Id");
        command.AddParameter("@Id", pessoaJuridica.Id);
        command.AddParameter("@CNPJ", pessoaJuridica.Cnpj);
        command.AddParameter("@Inscricao_Estadual", pessoaJuridica.InscricaoEstadual);
        command.ExecuteNonQuery();

        return (PessoaJuridica)pessoaRepository.Update(pessoaJuridica);
    }

    public void Delete(PessoaJuridica pessoaJuridica)
    {
        using var command = commandFactory.Create("DELETE FROM Pessoas_Juridicas WHERE Id = @Id");
        command.AddParameter("@Id", pessoaJuridica.Id);
        command.ExecuteNonQuery();

        pessoaRepository.Delete(pessoaJuridica);
    }

    private PessoaJuridica CreatePessoaJuridica(SqlDataReader reader)
    {
        var id = (int)reader["Id"];
        var pessoa = pessoaRepository.Get(id);

        return new()
        {
            Id = id,
            Nome = pessoa.Nome,
            Observacoes = pessoa.Observacoes,
            Pix = pessoa.Pix,
            Email = pessoa.Email,
            Enderecos = pessoa.Enderecos,
            Telefones = pessoa.Telefones,
            Cnpj = (string)reader["CNPJ"],
            InscricaoEstadual = (string)reader["Inscricao_Estadual"]
        };
    }
}
