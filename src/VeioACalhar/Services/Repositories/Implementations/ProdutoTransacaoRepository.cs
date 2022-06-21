using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class ProdutoTransacaoRepository : IProdutoTransacaoRepository
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly IProdutoRepository produtoRepository;

    public ProdutoTransacaoRepository(ISqlCommandFactory commandFactory, IProdutoRepository produtoRepository)
    {
        this.commandFactory = commandFactory;
        this.produtoRepository = produtoRepository;
    }

    public IReadOnlyCollection<ProdutoTransacao> CreateFor(Transacao transacao)
    {
        var produtos = new List<ProdutoTransacao>();

        foreach (var produto in transacao.Produtos)
            produtos.Add(Create(produto, transacao));

        return produtos;
    }

    public IReadOnlyCollection<ProdutoTransacao> GetFor(Transacao transacao)
    {
        using var command = commandFactory.Create("SELECT * FROM Transacoes_Produtos WHERE Id_Transacoes = @Id_Transacoes");
        command.AddParameter("@Id_Transacoes", transacao.Id);

        using var reader = command.ExecuteReader();

        var produtos = new List<ProdutoTransacao>();
        while (reader.Read())
            produtos.Add(CreateProduto(reader));

        return produtos;
    }

    public IReadOnlyCollection<ProdutoTransacao> UpdateFor(Transacao transacao)
    {

        DeleteFor(transacao);
        return CreateFor(transacao);
    }

    public void DeleteFor(Transacao transacao)
    {
        using var command = commandFactory.Create("DELETE FROM Transacoes_Produtos WHERE Id_Transacoes = @Id_Transacoes");
        command.AddParameter("@Id_Transacoes", transacao.Id);

        command.ExecuteNonQuery();
    }

    private ProdutoTransacao Create(ProdutoTransacao produto, Transacao transacao)
    {
        using var command = commandFactory.Create("INSERT INTO Transacoes_Produtos (Id_Transacoes, Id_Produto, Quantidade, Valor_Unitario, Desconto_Unitario) OUTPUT INSERTED.Id VALUES (@Id_Transacoes, @Id_Produto, @Quantidade, @Valor_Unitario, @Desconto_Unitario)");
        command.AddParameter("@Id_Transacoes", transacao.Id);
        command.AddParameter("@Id_Produto", produto.Produto.Id);
        command.AddParameter("@Quantidade", produto.Quantidade);
        command.AddParameter("@Valor_Unitario", produto.ValorUnitario);
        command.AddParameter("@Desconto_Unitario", produto.DescontoUnitario);

        var id = command.ExecuteScalar<int>();

        return produto with { Id = id };
    }

    private ProdutoTransacao CreateProduto(SqlDataReader reader)
    {
        return new()
        {
            Id = reader.GetInt32(0),
            Produto = produtoRepository.Get(reader.GetInt32(2)),
            Quantidade = reader.GetDecimal(3),
            ValorUnitario = reader.GetSqlMoney(4).Value,
            DescontoUnitario = reader.GetInt32(5)
        };
    }
}
