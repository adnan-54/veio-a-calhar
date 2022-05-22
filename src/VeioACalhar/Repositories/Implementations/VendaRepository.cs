using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class VendaRepository : IVendaRepository
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly ITransacaoRepository<Venda> transacaoRepository;
    private readonly IVendaClienteRepository vendaClienteRepository;
    private readonly IVendaFuncionarioRepository vendaFuncionarioRepository;

    public VendaRepository(ISqlCommandFactory commandFactory, ITransacaoRepository<Venda> transacaoRepository, IVendaClienteRepository vendaClienteRepository, IVendaFuncionarioRepository vendaFuncionarioRepository)
    {
        this.commandFactory = commandFactory;
        this.transacaoRepository = transacaoRepository;
        this.vendaClienteRepository = vendaClienteRepository;
        this.vendaFuncionarioRepository = vendaFuncionarioRepository;
    }

    public Venda Create(Venda venda)
    {
        venda = transacaoRepository.Create(venda);
        
        using var command = commandFactory.Create("INSERT INTO Vendas (Id, Previsao_Inicio, Previsao_Entrega) VALUES (@Id, @Previsao_Inicio, @Previsao_Entrega)");
        command.AddParameter("@Id", venda.Id);
        command.AddParameter("@Previsao_Inicio", venda.PrevisaoInicio);
        command.AddParameter("@Previsao_Entrega", venda.PrevisaoEntrega);
        command.ExecuteNonQuery();

        vendaClienteRepository.CreateFor(venda);
        vendaFuncionarioRepository.CreateFor(venda);

        return venda;
    }

    public Venda Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Vendas WHERE Id = @Id");
        command.AddParameter("@Id", id);
        using var reader = command.ExecuteReader();
        
        if(reader.Read())
            return CreateVenda(reader);
        return new();
    }
    
    public IReadOnlyCollection<Venda> GetAll()
    {
        using var command = commandFactory.Create("SELECT * FROM Vendas");
        using var reader = command.ExecuteReader();

        var vendas = new List<Venda>();
        while (reader.Read())
            vendas.Add(CreateVenda(reader));
        return vendas;
    }

    public Venda Update(Venda venda)
    {
        venda = transacaoRepository.Update(venda);
        using var command = commandFactory.Create("UPDATE Vendas SET Previsao_Inicio = @Previsao_Inicio, Previsao_Entrega = @Previsao_Entrega WHERE Id = @Id");
        command.AddParameter("@Id", venda.Id);
        command.AddParameter("@Previsao_Inicio", venda.PrevisaoInicio);
        command.AddParameter("@Previsao_Entrega", venda.PrevisaoEntrega);
        command.ExecuteNonQuery();

        vendaClienteRepository.UpdateFor(venda);
        vendaFuncionarioRepository.UpdateFor(venda);

        return venda;
    }

    public void Delete(Venda venda)
    {
        vendaClienteRepository.DeleteFor(venda);
        vendaFuncionarioRepository.DeleteFor(venda);

        using var command = commandFactory.Create("DELETE FROM Vendas WHERE Id = @Id");
        command.AddParameter("@Id", venda.Id);
        command.ExecuteNonQuery();

        transacaoRepository.Delete(venda);
    }

    private Venda CreateVenda(SqlDataReader reader)
    {
        var id = reader.GetInt32(0);
        var venda = transacaoRepository.Get(id);

        return venda with
        {
            PrevisaoInicio = DateOnly.FromDateTime(reader.GetDateTime(1)),
            PrevisaoEntrega = DateOnly.FromDateTime(reader.GetDateTime(2)),
            Clientes = vendaClienteRepository.GetFor(venda),
            Funcionarios = vendaFuncionarioRepository.GetFor(venda)
        };

    }
}
