using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class FormaPagamentoRepository : IFormaPagamentoRepository
{
    private readonly ISqlCommandFactory commandFactory;

    public FormaPagamentoRepository(ISqlCommandFactory commandFactory)
    {
        this.commandFactory = commandFactory;
    }

    public FormaPagamento Create(FormaPagamento formaPagamento)
    {
        using var command = commandFactory.Create("INSERT INTO Formas_Pagamento(Nome, Maximo_Parcelas) OUTPUT INSERTED.Id VALUES (@Nome, @Maximo_Parcelas)");
        command.AddParameter("@Nome", formaPagamento.Nome);
        command.AddParameter("@Maximo_Parcelas", formaPagamento.MaximoParcelas);

        var id = command.ExecuteScalar<int>();

        return formaPagamento with { Id = id };
    }

    public FormaPagamento Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Formas_Pagamento WHERE Id = @Id");
        command.AddParameter("@Id", id);
        using var reader = command.ExecuteReader();

        if (reader.Read())
            return CreateFormaPagamento(reader);
        return new();
    }

    public IReadOnlyCollection<FormaPagamento> GetAll()
    {
        using var command = commandFactory.Create("SELECT * FROM Formas_Pagamento");
        using var reader = command.ExecuteReader();

        var formasPagamento = new List<FormaPagamento>();
        while (reader.Read())
            formasPagamento.Add(CreateFormaPagamento(reader));
        return formasPagamento;
    }

    public FormaPagamento Update(FormaPagamento formaPagamento)
    {
        using var command = commandFactory.Create("UPDATE Formas_Pagamento SET Nome = @Nome, Maximo_Parcelas = @Maximo_Parcelas WHERE Id = @Id");
        command.AddParameter("@Id", formaPagamento.Id);
        command.AddParameter("@Nome", formaPagamento.Nome);
        command.AddParameter("@Maximo_Parcelas", formaPagamento.MaximoParcelas);
        command.ExecuteNonQuery();

        return formaPagamento;
    }

    public void Delete(FormaPagamento formaPagamento)
    {
        using var command = commandFactory.Create("DELETE FROM Formas_Pagamento WHERE Id = @Id");
        command.AddParameter("@Id", formaPagamento.Id);
        command.ExecuteNonQuery();
    }

    private static FormaPagamento CreateFormaPagamento(SqlDataReader reader)
    {
        return new()
        {
            Id = reader.GetInt32(0),
            Nome = reader.GetString(1),
            MaximoParcelas = reader.GetInt32(2)
        };
    }
}