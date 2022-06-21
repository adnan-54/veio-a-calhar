using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly ITransacaoRepository<Venda> vendaRepository;
    private readonly ITransacaoRepository<Compra> compraRepository;

    public TransacaoRepository(ITransacaoRepository<Venda> vendaRepository, ITransacaoRepository<Compra> compraRepository)
    {
        this.vendaRepository = vendaRepository;
        this.compraRepository = compraRepository;
    }

    public Transacao Get(int id)
    {
        return vendaRepository.Get(id) as Transacao ?? compraRepository.Get(id);
    }

    public IReadOnlyCollection<Transacao> GetAll()
    {
        return vendaRepository.GetAll().Cast<Transacao>().Concat(compraRepository.GetAll()).ToList();
    }
}

public class TransacaoRepository<TTransacao> : ITransacaoRepository<TTransacao> where TTransacao : Transacao, new()
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly IStatusTransacaoRepository statusTransacaoRepository;
    private readonly IProdutoTransacaoRepository produtoTransacaoRepository;

    public TransacaoRepository(ISqlCommandFactory commandFactory, IStatusTransacaoRepository statusTransacaoRepository, IProdutoTransacaoRepository produtoTransacaoRepository)
    {
        this.commandFactory = commandFactory;
        this.statusTransacaoRepository = statusTransacaoRepository;
        this.produtoTransacaoRepository = produtoTransacaoRepository;
    }

    public TTransacao Create(TTransacao transacao)
    {
        using var command = commandFactory.Create("INSERT INTO Transacoes (Id_Status, Data_Criacao, Data_Fechamento, Observacoes) OUTPUT INSERTED.Id VALUES (@Id_Status, @Data_Criacao, @Data_Fechamento, @Observacoes)");
        command.AddParameter("@Id_Status", transacao.Status.Id);
        command.AddParameter("@Data_Criacao", transacao.DataCriacao.ToDateTime(default));
        command.AddParameter("@Data_Fechamento", transacao.DataFechamento?.ToDateTime(default));
        command.AddParameter("@Observacoes", transacao.Observacoes);

        var id = command.ExecuteScalar<int>();

        transacao = transacao with { Id = id };

        var produtos = produtoTransacaoRepository.CreateFor(transacao);

        return transacao with { Produtos = produtos };
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
        using var command = commandFactory.Create("UPDATE Transacoes SET Id_Status = @Id_Status, Data_Fechamento = @Data_Fechamento, Observacoes = @Observacoes WHERE Id = @Id");
        command.AddParameter("@Id", transacao.Id);
        command.AddParameter("@Id_Status", transacao.Status.Id);
        command.AddParameter("@Data_Fechamento", transacao.DataFechamento?.ToDateTime(default));
        command.AddParameter("@Observacoes", transacao.Observacoes);

        command.ExecuteNonQuery();

        var produtos = produtoTransacaoRepository.UpdateFor(transacao);

        return transacao with { Produtos = produtos };
    }

    public void Delete(TTransacao transacao)
    {
        produtoTransacaoRepository.DeleteFor(transacao);

        using var command = commandFactory.Create("DELETE FROM Transacoes WHERE Id = @Id");
        command.AddParameter("@Id", transacao.Id);

        command.ExecuteNonQuery();
    }

    private TTransacao CreateTransacao(SqlDataReader reader)
    {
        var transacao = new TTransacao()
        {
            Id = reader.GetInt32(0),
            Status = statusTransacaoRepository.Get(reader.GetInt32(1)),
            DataCriacao = DateOnly.FromDateTime(reader.GetDateTime(2)),
            DataFechamento = reader.IsDBNull(3) ? null : DateOnly.FromDateTime(reader.GetDateTime(3)),
            Observacoes = reader.GetString(4),
        };

        var produtos = produtoTransacaoRepository.GetFor(transacao);

        return transacao with { Produtos = produtos };
    }
}
