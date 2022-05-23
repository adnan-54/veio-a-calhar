using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class StatusTransacaoRepository : IStatusTransacaoRepository
{
    private readonly ISqlCommandFactory commandFactory;

    public StatusTransacaoRepository(ISqlCommandFactory commandFactory)
    {
        this.commandFactory = commandFactory;
    }

    public StatusTransacao Create(StatusTransacao statusTransacao)
    {
        using var command = commandFactory.Create("INSERT INTO Status_Transacaos(Status) OUTPUT INSERTED.Id VALUES (@Status)");
        command.AddParameter("@Status", statusTransacao.Status);

        var id = command.ExecuteScalar<int>();

        return statusTransacao with { Id = id };
    }

    public StatusTransacao Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Status_Transacaos WHERE Id = @Id");
        command.AddParameter("@Id", id);
        using var reader = command.ExecuteReader();

        if (reader.Read())
            return CreateStatusTransacao(reader);
        return new();
    }

    public IReadOnlyCollection<StatusTransacao> GetAll()
    {
        using var command = commandFactory.Create("SELECT * FROM Status_Transacaos");
        using var reader = command.ExecuteReader();

        var statusTransacaos = new List<StatusTransacao>();
        while (reader.Read())
            statusTransacaos.Add(CreateStatusTransacao(reader));
        return statusTransacaos;
    }

    public StatusTransacao Update(StatusTransacao statusTransacao)
    {
        using var command = commandFactory.Create("UPDATE Status_Transacaos SET Status = @Status WHERE Id = @Id");
        command.AddParameter("@Id", statusTransacao.Id);
        command.AddParameter("@Status", statusTransacao.Status);
        command.ExecuteNonQuery();

        return statusTransacao;
    }

    public void Delete(StatusTransacao statusTransacao)
    {
        using var command = commandFactory.Create("DELETE FROM Status_Transacaos WHERE Id = @Id");
        command.AddParameter("@Id", statusTransacao.Id);
        command.ExecuteNonQuery();
    }

    private static StatusTransacao CreateStatusTransacao(SqlDataReader reader)
    {
        return new()
        {
            Id = reader.GetInt32(0),
            Status = reader.GetString(1)
        };
    }
}
