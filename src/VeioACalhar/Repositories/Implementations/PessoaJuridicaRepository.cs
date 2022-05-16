using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class PessoaJuridicaRepository<TPessoa> : IPessoaJuridicaRepository<TPessoa> where TPessoa : PessoaJuridica, new()
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly IPessoaRepository<TPessoa> pessoaRepository;

    public PessoaJuridicaRepository(ISqlCommandFactory commandFactory, IPessoaRepository<TPessoa> pessoaRepository)
    {
        this.commandFactory = commandFactory;
        this.pessoaRepository = pessoaRepository;
    }

    public TPessoa Create(TPessoa pessoa)
    {
        pessoa = pessoaRepository.Create(pessoa);

        using var command = commandFactory.Create("INSERT INTO Pessoas_Juridicas (Id, Nome_Fantasia, Inscricao_Estadual, Cnpj) VALUES (@Id, @Nome_Fantasia, @Inscricao_Estadual, @Cnpj)");
        command.AddParameter("@Id", pessoa.Id);
        command.AddParameter("@Nome_Fantasia", pessoa.NomeFantasia);
        command.AddParameter("@Inscricao_Estadual", pessoa.InscricaoEstadual);
        command.AddParameter("@Cnpj", pessoa.Cnpj);

        command.ExecuteNonQuery();

        return pessoa;

    }

    public TPessoa Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas_Juridicas WHERE Id = @Id");
        command.AddParameter("@Id", id);
        using var reader = command.ExecuteReader();

        if (reader.Read())
            return CreatePessoa(reader);
        return new();

    }

    public IEnumerable<TPessoa> GetAll()
    {
        using var command = commandFactory.Create("SELECT * FROM Pessoas_Juridicas");
        using var reader = command.ExecuteReader();
        while (reader.Read())
            yield return CreatePessoa(reader);
    }

    public TPessoa Update(TPessoa pessoa)
    {
        pessoa = pessoaRepository.Update(pessoa);
        using var command = commandFactory.Create("-");
        command.AddParameter("@Id", pessoa.Id);
        command.AddParameter("@Nome_Fantasia", pessoa.NomeFantasia);
        command.AddParameter("@Inscricao_Estadual", pessoa.InscricaoEstadual);
        command.AddParameter("@Cnpj", pessoa.Cnpj);

        command.ExecuteNonQuery();

        return pessoa;
    }

    public void Delete(TPessoa pessoa)
    {
        using var command = commandFactory.Create("DELETE FROM Pessoas_Juridicas WHERE Id = @Id");
        command.AddParameter("@Id", pessoa.Id);
        command.ExecuteNonQuery();

        pessoaRepository.Delete(pessoa);
    }

    private TPessoa CreatePessoa(SqlDataReader reader)
    {
        var id = (int)reader["Id"];
        var pessoa = pessoaRepository.Get(id);

        return pessoa with
        {
            NomeFantasia = (string)reader["Nome_Fantasia"],
            InscricaoEstadual = (string)reader["Inscricao_Estadual"],
            Cnpj = (string)reader["Cnpj"]
        };
    }
}
