using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class VendaClienteRepository : IVendaClienteRepository
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly IClienteRepository clienteRepository;

    public VendaClienteRepository(ISqlCommandFactory commandFactory, IClienteRepository clienteRepository)
    {
        this.commandFactory = commandFactory;
        this.clienteRepository = clienteRepository;
    }

    public void CreateFor(Venda venda)
    {
        foreach (var cliente in venda.Clientes)
            Create(cliente, venda);
    }

    public IReadOnlyCollection<Pessoa> GetFor(Venda venda)
    {
        using var command = commandFactory.Create("SELECT * FROM Vendas_Clientes WHERE Id_Venda = @Id_Venda");
        command.AddParameter("@Id_Venda", venda.Id);
        using var reader = command.ExecuteReader();

        var clientes = new List<Pessoa>();
        while (reader.Read())
            clientes.Add(clienteRepository.Get(reader.GetInt32(2)));
        return clientes;
    }

    public void UpdateFor(Venda venda)
    {
        DeleteFor(venda);
        CreateFor(venda);
    }

    public void DeleteFor(Venda venda)
    {
        using var command = commandFactory.Create("DELETE FROM Venda_Clientes WHERE Id_Venda = @Id_Venda");
        command.AddParameter("@Id_Venda", venda.Id);
        command.ExecuteNonQuery();
    }

    private void Create(Pessoa cliente, Venda venda)
    {
        using var command = commandFactory.Create("INSERT INTO Venda_Clientes (Id_Venda, Id_Cliente) VALUES (@Id_Venda, @Id_Cliente)");
        command.AddParameter("@Id_Venda", venda.Id);
        command.AddParameter("@Id_Cliente", cliente.Id);
        command.ExecuteNonQuery();
    }
}