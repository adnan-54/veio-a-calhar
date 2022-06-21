using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly IPessoaFisicaRepository<Cliente> pessoaRepository;

    public ClienteRepository(ISqlCommandFactory commandFactory, IPessoaFisicaRepository<Cliente> pessoaRepository)
    {
        this.commandFactory = commandFactory;
        this.pessoaRepository = pessoaRepository;
    }

    public Cliente Create(Cliente cliente)
    {
        cliente = pessoaRepository.Create(cliente);

        using var command = commandFactory.Create("INSERT INTO Clientes(Id) VALUES (@Id)");
        command.AddParameter("@Id", cliente.Id);
        command.ExecuteNonQuery();

        return cliente;
    }

    public Cliente Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Clientes WHERE Id = @Id");
        command.AddParameter("@Id", id);

        if (command.ExecuteScalar<int>() == id)
            return pessoaRepository.Get(id);
        return new();
    }

    public IReadOnlyCollection<Cliente> GetAll()
    {
        using var command = commandFactory.Create("SELECT * FROM Clientes");
        using var reader = command.ExecuteReader();

        var ids = new List<int>();
        while (reader.Read())
            ids.Add(reader.GetInt32(0));

        return pessoaRepository.GetAll().Where(cliente => ids.Contains(cliente.Id)).ToList();
    }

    public Cliente Update(Cliente cliente)
    {
        return pessoaRepository.Update(cliente);
    }

    public void Delete(Cliente cliente)
    {
        using var command = commandFactory.Create("DELETE FROM Clientes WHERE Id = @Id");
        command.AddParameter("@Id", cliente.Id);
        command.ExecuteNonQuery();
        pessoaRepository.Delete(cliente);
    }
}
