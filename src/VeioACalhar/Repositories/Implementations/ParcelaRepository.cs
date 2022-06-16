using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class ParcelaRepository : IParcelaRepository
{
    private readonly ISqlCommandFactory commandFactory;

    public ParcelaRepository(ISqlCommandFactory commandFactory)
    {
        this.commandFactory = commandFactory;
    }

    public IReadOnlyCollection<Parcela> CreateFrom(Pagamento pagamento)
    {
        var parcelas = new List<Parcela>();

        foreach (var parcela in pagamento.Parcelas)
            parcelas.Add(Create(parcela, pagamento));

        return parcelas;
    }

    public IReadOnlyCollection<Parcela> GetFrom(Pagamento pagamento)
    {
        using var command = commandFactory.Create("SELECT * FROM Parcelas WHERE Id_Pagamento = @Id_Pagamento");
        command.AddParameter("@Id_Pagamento", pagamento.Id);
        using var reader = command.ExecuteReader();

        var parcelas = new List<Parcela>();
        while (reader.Read())
            parcelas.Add(CreateParcela(reader));
        return parcelas;
    }

    public IReadOnlyCollection<Parcela> UpdateFrom(Pagamento pagamento)
    {
        DeleteFrom(pagamento);
        return CreateFrom(pagamento);
    }

    public void DeleteFrom(Pagamento pagamento)
    {
        using var command = commandFactory.Create("DELETE FROM Parcelas WHERE Id_Pagamento = @Id_Pagamento");
        command.AddParameter("@Id_Pagamento", pagamento.Id);
        command.ExecuteNonQuery();
    }

    private Parcela Create(Parcela parcela, Pagamento pagamento)
    {
        using var command = commandFactory.Create("INSERT INTO Parcelas(Id_Pagamento, Numero, Valor, Porcentagem_Desconto, Valor_Pago, Data_Vencimento, Data_Pagamento) OUTPUT INSERTED.Id VALUES (@Id_Pagamento, @Numero, @Valor, @Porcentagem_Desconto, @Valor_Pago, @Data_Vencimento, @Data_Pagamento)");
        command.AddParameter("@Id_Pagamento", pagamento.Id);
        command.AddParameter("@Numero", parcela.Numero);
        command.AddParameter("@Valor", parcela.Valor);
        command.AddParameter("@Porcentagem_Desconto", parcela.PorcentagemDesconto);
        command.AddParameter("@Valor_Pago", parcela.ValorPago);
        command.AddParameter("@Data_Vencimento", parcela.DataVencimento.ToDateTime(default));
        command.AddParameter("@Data_Pagamento", parcela.DataPagamento.ToDateTime(default));

        var id = command.ExecuteScalar<int>();

        return parcela with { Id = id };
    }

    private static Parcela CreateParcela(SqlDataReader reader)
    {
        return new()
        {
            Id = reader.GetInt32(0),
            Numero = reader.GetInt32(2),
            Valor = reader.GetSqlMoney(3).Value,
            PorcentagemDesconto = reader.GetInt32(4),
            ValorPago = reader.GetSqlMoney(5).Value,
            DataVencimento = DateOnly.FromDateTime(reader.GetDateTime(6)),
            DataPagamento = DateOnly.FromDateTime(reader.GetDateTime(7))
        };
    }
}
