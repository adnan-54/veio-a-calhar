using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly IFornecedorRepository fornecedorRepository;
    private readonly IUnidadeRepository unidadeRepository;

    public ProdutoRepository(ISqlCommandFactory commandFactory, IFornecedorRepository fornecedorRepository, IUnidadeRepository unidadeRepository)
    {
        this.commandFactory = commandFactory;
        this.fornecedorRepository = fornecedorRepository;
        this.unidadeRepository = unidadeRepository;
    }

    public Produto Create(Produto produto)
    {
        using var command = commandFactory.Create("INSERT INTO Produtos(Id_Fornecedor, Id_Unidade, Nome, Descricao, Preco_Custo, Preco_Venda, Quantidade) OUTPUT INSERTED.Id VALUES (@Id_Fornecedor, @Id_Unidade, @Nome, @Descricao, @Preco_Custo, @Preco_Venda, @Quantidade)");
        command.AddParameter("@Id_Fornecedor", produto.Fornecedor.Id);
        command.AddParameter("@Id_Unidade", produto.Unidade.Id);
        command.AddParameter("@Nome", produto.Nome);
        command.AddParameter("@Descricao", produto.Descricao);
        command.AddParameter("@Preco_Custo", produto.PrecoCusto);
        command.AddParameter("@Preco_Venda", produto.PrecoVenda);
        command.AddParameter("@Quantidade", produto.Quantidade);

        var id = command.ExecuteScalar<int>();

        return produto with { Id = id };
    }

    public Produto Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Produtos WHERE Id = @Id");
        command.AddParameter("@Id", id);
        using var reader = command.ExecuteReader();

        if (reader.Read())
            return CreateProduto(reader);
        return new();
    }

    public IReadOnlyCollection<Produto> GetAll()
    {
        using var command = commandFactory.Create("SELECT * FROM Produtos");
        using var reader = command.ExecuteReader();

        var produtos = new List<Produto>();
        while (reader.Read())
            produtos.Add(CreateProduto(reader));
        return produtos;
    }

    public Produto Update(Produto produto)
    {
        using var command = commandFactory.Create("UPDATE Produtos SET Id_Fornecedor = @Id_Fornecedor, Id_Unidade = @Id_Unidade, Nome = @Nome, Descricao = @Descricao, Preco_Custo = @Preco_Custo, Preco_Venda = @Preco_Venda, Quantidade = @Quantidade WHERE Id = @Id");
        command.AddParameter("@Id", produto.Id);
        command.AddParameter("@Id_Fornecedor", produto.Fornecedor.Id);
        command.AddParameter("@Id_Unidade", produto.Unidade.Id);
        command.AddParameter("@Nome", produto.Nome);
        command.AddParameter("@Descricao", produto.Descricao);
        command.AddParameter("@Preco_Custo", produto.PrecoCusto);
        command.AddParameter("@Preco_Venda", produto.PrecoVenda);
        command.AddParameter("@Quantidade", produto.Quantidade);
        command.ExecuteNonQuery();

        return produto;
    }

    public void Delete(Produto produto)
    {
        using var command = commandFactory.Create("DELETE FROM Produtos WHERE Id = @Id");
        command.AddParameter("@Id", produto.Id);
        command.ExecuteNonQuery();
    }

    private Produto CreateProduto(SqlDataReader reader)
    {
        return new()
        {
            Id = reader.GetInt32(0),
            Fornecedor = fornecedorRepository.Get(reader.GetInt32(1)),
            Unidade = unidadeRepository.Get(reader.GetInt32(2)),
            Nome = reader.GetString(3),
            Descricao = reader.GetString(4),
            PrecoCusto = reader.GetSqlMoney(5).Value,
            PrecoVenda = reader.GetSqlMoney(6).Value,
            Quantidade = reader.GetDecimal(7)
        };
    }
}
