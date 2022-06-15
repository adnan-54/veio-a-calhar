using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class PagamentoRepository : IPagamentoRepository
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly ITransacaoRepository transacaoRepository;
    private readonly IParcelaRepository parcelasRepository;
    private readonly IFormaPagamentoRepository formaPagamentoRepository;
    private readonly IPessoaRepository<PessoaGenerica> pessoaRepository;

    public PagamentoRepository(ISqlCommandFactory commandFactory, ITransacaoRepository transacaoRepository, IParcelaRepository parcelasRepository, IFormaPagamentoRepository formaPagamentoRepository, IPessoaRepository<PessoaGenerica> pessoaRepository)
    {
        this.commandFactory = commandFactory;
        this.transacaoRepository = transacaoRepository;
        this.parcelasRepository = parcelasRepository;
        this.formaPagamentoRepository = formaPagamentoRepository;
        this.pessoaRepository = pessoaRepository;
    }

    public Pagamento Create(Pagamento pagamento)
    {
        using var command = commandFactory.Create("INSERT INTO Pagamentos(Id_Transacao, Id_Pagador, Id_Favorecido, Id_Forma_Pagamento) OUTPUT INSERTED.Id VALUES (@Id_Transacao, @Id_Pagador, @Id_Favorecido, @Id_Forma_Pagamento)");
        command.AddParameter("@Id_Transacao", pagamento.Transacao.Id);
        command.AddParameter("@Id_Pagador", pagamento.Pagador.Id);
        command.AddParameter("@Id_Favorecido", pagamento.Favorecido.Id);
        command.AddParameter("@Id_Forma_Pagamento", pagamento.FormaPagamento.Id);

        var id = command.ExecuteScalar<int>();

        pagamento = pagamento with { Id = id };

        var parcelas = parcelasRepository.CreateFrom(pagamento);

        return pagamento with { Parcelas = parcelas };
    }

    public Pagamento Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Pagamentos WHERE Id = @Id");
        command.AddParameter("@Id", id);
        using var reader = command.ExecuteReader();

        if (reader.Read())
            return CreatePagamento(reader);
        return new();
    }

    public IReadOnlyCollection<Pagamento> GetAll()
    {
        using var command = commandFactory.Create("SELECT * FROM Pagamentos");
        using var reader = command.ExecuteReader();

        var pagamentos = new List<Pagamento>();
        while (reader.Read())
            pagamentos.Add(CreatePagamento(reader));
        return pagamentos;
    }

    public Pagamento Update(Pagamento pagamento)
    {
        using var command = commandFactory.Create("UPDATE Pagamentos SET Id_Transacao = @Id_Transacao, Id_Pagador = @Id_Pagador, Id_Favorecido = @Id_Favorecido, Id_Forma_Pagamento = @Id_Forma_Pagamento WHERE Id = @Id");
        command.AddParameter("@Id", pagamento.Id);
        command.AddParameter("@Id_Transacao", pagamento.Transacao.Id);
        command.AddParameter("@Id_Pagador", pagamento.Pagador.Id);
        command.AddParameter("@Id_Favorecido", pagamento.Favorecido.Id);
        command.AddParameter("@Id_Forma_Pagamento", pagamento.FormaPagamento.Id);
        command.ExecuteNonQuery();

        var parcelas = parcelasRepository.UpdateFrom(pagamento);

        return pagamento with { Parcelas = parcelas };
    }

    public void Delete(Pagamento pagamento)
    {
        using var command = commandFactory.Create("DELETE FROM Pagamentos WHERE Id = @Id");
        command.AddParameter("@Id", pagamento.Id);
        command.ExecuteNonQuery();
    }

    private Pagamento CreatePagamento(SqlDataReader reader)
    {
        var pagamento = new Pagamento
        {
            Id = reader.GetInt32(0),
            Transacao = transacaoRepository.Get(reader.GetInt32(1)),
            Pagador = pessoaRepository.Get(reader.GetInt32(2)),
            Favorecido = pessoaRepository.Get(reader.GetInt32(3)),
            FormaPagamento = formaPagamentoRepository.Get(reader.GetInt32(4))
        };

        var parcelas = parcelasRepository.GetFrom(pagamento);

        return pagamento with { Parcelas = parcelas };
    }
}
