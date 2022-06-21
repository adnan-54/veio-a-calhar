using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class FornecedorRepository : IFornecedorRepository
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly IPessoaJuridicaRepository<Fornecedor> pessoaRepository;

    public FornecedorRepository(ISqlCommandFactory commandFactory, IPessoaJuridicaRepository<Fornecedor> pessoaRepository)
    {
        this.commandFactory = commandFactory;
        this.pessoaRepository = pessoaRepository;
    }

    public Fornecedor Create(Fornecedor fornecedor)
    {
        fornecedor = pessoaRepository.Create(fornecedor);

        using var command = commandFactory.Create("INSERT INTO Fornecedores(Id) VALUES (@Id)");
        command.AddParameter("@Id", fornecedor.Id);
        command.ExecuteNonQuery();

        return fornecedor;
    }

    public Fornecedor Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Fornecedores WHERE Id = @Id");
        command.AddParameter("@Id", id);

        if (command.ExecuteScalar<int>() == id)
            return pessoaRepository.Get(id);
        return new();
    }

    public IReadOnlyCollection<Fornecedor> GetAll()
    {
        using var command = commandFactory.Create("SELECT * FROM Fornecedores");
        using var reader = command.ExecuteReader();

        var ids = new List<int>();
        while (reader.Read())
            ids.Add(reader.GetInt32(0));

        return pessoaRepository.GetAll().Where(cliente => ids.Contains(cliente.Id)).ToList();
    }

    public Fornecedor Update(Fornecedor fornecedor)
    {
        return pessoaRepository.Update(fornecedor);
    }

    public void Delete(Fornecedor fornecedor)
    {
        using var command = commandFactory.Create("DELETE FROM Fornecedores WHERE Id = @Id");
        command.AddParameter("@Id", fornecedor.Id);
        command.ExecuteNonQuery();
        pessoaRepository.Delete(fornecedor);
    }
}
