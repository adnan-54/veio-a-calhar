using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class CompraRepository : ICompraRepository
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly ITransacaoRepository<Compra> transacaoRepository;
    private readonly IFornecedorRepository fornecedorRepository;

    public CompraRepository(ISqlCommandFactory commandFactory, ITransacaoRepository<Compra> transacaoRepository, IFornecedorRepository fornecedorRepository)
    {
        this.commandFactory = commandFactory;
        this.transacaoRepository = transacaoRepository;
        this.fornecedorRepository = fornecedorRepository;
    }

    public Compra Create(Compra compra)
    {
        compra = transacaoRepository.Create(compra);
        using var command = commandFactory.Create("INSERT INTO Compras (Id, Id_Fornecedor, Data_Compra, Data_Entrega) VALUES (@Id, Id_Fornecedor, Data_Compra, Data_Entrega)");
        command.AddParameter("@Id", compra.Id);
        command.AddParameter("@Id_Fornecedor", compra.Fornecedor.Id);
        command.AddParameter("@Data_Compra", compra.DataCompra);
        command.AddParameter("@Data_Entrega", compra.DataEntrega);
        command.ExecuteNonQuery();

        return compra;

    }

    public Compra Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Compras WHERE Id = @Id");
        command.AddParameter("@Id", id);
        using var reader = command.ExecuteReader();

        if (reader.Read())
            return CreateCompra(reader);
        return new();
    }

    public IReadOnlyCollection<Compra> GetAll()
    {
        using var command = commandFactory.Create("SELECT * FROM Compras");
        using var reader = command.ExecuteReader();

        var compras = new List<Compra>();
        while (reader.Read())
            compras.Add(CreateCompra(reader));
        return compras;
    }

    public Compra Update(Compra compra)
    {
        compra = transacaoRepository.Update(compra);
        using var command = commandFactory.Create("UPDATE Compras SET Id_Fornecedor = @Id_Fornecedor, Data_Compra = @Data_Compra, Data_Entrega = @Data_Entrega WHERE Id = @Id");
        command.AddParameter("@Id", compra.Id);
        command.AddParameter("@Id_Fornecedor", compra.Fornecedor.Id);
        command.AddParameter("@Data_Compra", compra.DataCompra);
        command.AddParameter("@Data_Entrega", compra.DataEntrega);
        command.ExecuteNonQuery();

        return compra;
    }

    public void Delete(Compra compra)
    {
        using var command = commandFactory.Create("DELETE FROM Compras WHERE Id = @Id");
        command.AddParameter("@Id", compra.Id);
        command.ExecuteNonQuery();

        transacaoRepository.Delete(compra);
    }

    private Compra CreateCompra(SqlDataReader reader)
    {
        var id = reader.GetInt32(0);
        var compra = transacaoRepository.Get(id);

        return compra with
        {
            Fornecedor = fornecedorRepository.Get(reader.GetInt32(1)),
            DataCompra = DateOnly.FromDateTime(reader.GetDateTime(2)),
            DataEntrega = DateOnly.FromDateTime(reader.GetDateTime(3))
        };
    }
}
