using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

//todo: fazer sql para esse repositorio
public class ClienteFisicoRepository : IClienteFisicoRepository
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly IPessoaFisicaRepository<ClienteFisico> pessoaRepository;

    public ClienteFisicoRepository(ISqlCommandFactory commandFactory, IPessoaFisicaRepository<ClienteFisico> pessoaRepository)
    {
        this.commandFactory = commandFactory;
        this.pessoaRepository = pessoaRepository;
    }

    public ClienteFisico Create(ClienteFisico cliente)
    {
        cliente = pessoaRepository.Create(cliente);
        
        using var command = commandFactory.Create("INSERT INTO Clientes_Fisicos(Id) VALUES (@Id)");
        command.AddParameter("@Id", cliente.Id);
        command.ExecuteNonQuery();

        return cliente;
    }

    public ClienteFisico Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Clientes_Fisicos WHERE Id = @Id");
        command.AddParameter("@Id", id);

        if (command.ExecuteScalar<int>() == id)
            return pessoaRepository.Get(id);
        return new();
    }

    public IReadOnlyCollection<ClienteFisico> GetAll()
    {
        using var command = commandFactory.Create("SELECT * FROM Clientes_Fisicos");
        using var reader = command.ExecuteReader();

        var ids = new List<int>();
        while (reader.Read())
            ids.Add(reader.GetInt32(0));

        return pessoaRepository.GetAll().Where(cliente => ids.Contains(cliente.Id)).ToList();
    }

    public ClienteFisico Update(ClienteFisico cliente)
    {
        return pessoaRepository.Update(cliente);
    }

    public void Delete(ClienteFisico cliente)
    {
        using var command = commandFactory.Create("DELETE FROM Clientes_Fisicos WHERE Id = @Id");
        command.AddParameter("@Id", cliente.Id);
        command.ExecuteNonQuery();
        pessoaRepository.Delete(cliente);
    }
}
