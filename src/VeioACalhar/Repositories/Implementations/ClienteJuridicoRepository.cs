using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

//todo: fazer sql para esse repositorio
public class ClienteJuridicoRepository : IClienteJuridicoRepository
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly IPessoaJuridicaRepository<ClienteJuridico> pessoaRepository;

    public ClienteJuridicoRepository(ISqlCommandFactory commandFactory, IPessoaJuridicaRepository<ClienteJuridico> pessoaRepository)
    {
        this.commandFactory = commandFactory;
        this.pessoaRepository = pessoaRepository;
    }

    public ClienteJuridico Create(ClienteJuridico cliente)
    {
        cliente = pessoaRepository.Create(cliente);
        
        using var command = commandFactory.Create("INSERT INTO Clientes_Juridicos(Id) VALUES (@Id)");
        command.AddParameter("@Id", cliente.Id);
        command.ExecuteNonQuery();

        return cliente;
    }

    public ClienteJuridico Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Clientes_Juridicos WHERE Id = @Id");
        command.AddParameter("@Id", id);
        
        if(command.ExecuteScalar<int>() == id)
            return pessoaRepository.Get(id);
        return new();
    }

    public IReadOnlyCollection<ClienteJuridico> GetAll()
    {
        using var command = commandFactory.Create("SELECT * FROM Clientes_Juridicos");
        using var reader = command.ExecuteReader();

        var ids = new List<int>();
        while (reader.Read())
            ids.Add(reader.GetInt32(0));

        return pessoaRepository.GetAll().Where(cliente => ids.Contains(cliente.Id)).ToList();
    }

    public ClienteJuridico Update(ClienteJuridico cliente)
    {
        return pessoaRepository.Update(cliente);
    }

    public void Delete(ClienteJuridico cliente)
    {
        using var command = commandFactory.Create("DELETE FROM Clientes_Juridicos WHERE Id = @Id");
        command.AddParameter("@Id", cliente.Id);
        command.ExecuteNonQuery();
        pessoaRepository.Delete(cliente);
    }
}
