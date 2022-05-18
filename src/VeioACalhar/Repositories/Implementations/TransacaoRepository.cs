using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class TransacaoRepository<TTransacao> : ITransacaoRepository<TTransacao> where TTransacao : Transacao, new()
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly IPagamentoRepository pagamentoRepository;
    private readonly IStatusTransacaoRepository statusTransacaoRepository;
    private readonly IProdutoTransacaoRepository produtoTransacaoRepository;

    public TransacaoRepository(ISqlCommandFactory commandFactory, IPagamentoRepository pagamentoRepository, IStatusTransacaoRepository statusTransacaoRepository, IProdutoTransacaoRepository produtoTransacaoRepository)
    {
        this.commandFactory = commandFactory;
        this.pagamentoRepository = pagamentoRepository;
        this.statusTransacaoRepository = statusTransacaoRepository;
        this.produtoTransacaoRepository = produtoTransacaoRepository;
    }

    public TTransacao Create(TTransacao transacao)
    {
        using var command = commandFactory.Create("INSERT INTO Transacoes (Id_Pagamento, Id_Status, Data_Criacao, Data_Fechamento, Observacoes) OUTPUT INSERTED.Id VALUES (@Id_Pagamento, @Id_Status, @Data_Criacao, @Data_Fechamento, @Observacoes)");
        command.AddParameter("@Id_Pagamento", transacao.Pagamento.Id);
        command.AddParameter("@Id_Status", transacao.Status.Id);
        command.AddParameter("@Data_Criacao", DateTime.Today);
        command.AddParameter("@Data_Fechamento", transacao.DataFechamento);
        command.AddParameter("@Observacoes", transacao.Observacoes);

        var id = command.ExecuteScalar<int>();

        transacao = transacao with { Id = id };

        var produtos = produtoTransacaoRepository.CreateFrom(transacao);

        return transacao with { DataCriacao = DateOnly.FromDateTime(DateTime.Today), Produtos = produtos };

    }

    public TTransacao Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Transacoes WHERE Id = @Id");
        command.AddParameter("@Id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
            return CreateTransacao(reader);
        return new();
    }

    public IReadOnlyCollection<TTransacao> GetAll()
    {
        using var command = commandFactory.Create("SELECT * FROM Transacoes");
        using var reader = command.ExecuteReader();

        var transacoes = new List<TTransacao>();
        while (reader.Read())
            transacoes.Add(CreateTransacao(reader));

        return transacoes;
    }

    public TTransacao Update(TTransacao transacao)
    {
        using var command = commandFactory.Create("UPDATE Transacoes SET Id_Pagamento = @Id_Pagamento, Id_Status = @Id_Status, Data_Criacao = @Data_Criacao, Data_Fechamento = @Data_Fechamento, Observacoes = @Observacoes WHERE Id = @Id");
        command.AddParameter("@Id", transacao.Id);
        command.AddParameter("@Id_Pagamento", transacao.Pagamento.Id);
        command.AddParameter("@Id_Status", transacao.Status.Id);
        command.AddParameter("@Data_Criacao", transacao.DataCriacao);
        command.AddParameter("@Data_Fechamento", transacao.DataFechamento);
        command.AddParameter("@Observacoes", transacao.Observacoes);

        command.ExecuteNonQuery();

        var produtos = produtoTransacaoRepository.UpdateFrom(transacao);

        return transacao with { Produtos = produtos };
    }

    public void Delete(TTransacao transacao)
    {
        produtoTransacaoRepository.DeleteFrom(transacao);

        using var command = commandFactory.Create("DELETE FROM Transacoes WHERE Id = @Id");
        command.AddParameter("@Id", transacao.Id);

        command.ExecuteNonQuery();
    }

    private TTransacao CreateTransacao(SqlDataReader reader)
    {
        var transacao = new TTransacao()
        {
            Id = reader.GetInt32(0),
            Pagamento = pagamentoRepository.Get(reader.GetInt32(1)),
            Status = statusTransacaoRepository.Get(reader.GetInt32(2)),
            DataCriacao = DateOnly.FromDateTime(reader.GetDateTime(3)),
            DataFechamento = DateOnly.FromDateTime(reader.GetDateTime(4)),
            Observacoes = reader.GetString(5)
        };

        var produtos = produtoTransacaoRepository.GetFrom(transacao);

        return transacao with { Produtos = produtos };
    }
}

public class ProdutoTransacaoRepository : IProdutoTransacaoRepository
{

}

public interface IProdutoTransacaoRepository
{
    IReadOnlyCollection<ProdutoTransacao> CreateFrom(Transacao transacao);

    IReadOnlyCollection<ProdutoTransacao> GetFrom(Transacao transacao);

    IReadOnlyCollection<ProdutoTransacao> UpdateFrom(Transacao transacao);

    void DeleteFrom(Transacao transacao);
}