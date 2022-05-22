using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class VendaFuncionarioRepository : IVendaFuncionarioRepository
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly IFuncionarioRepository funcionarioRepository;

    public VendaFuncionarioRepository(ISqlCommandFactory commandFactory, IFuncionarioRepository funcionarioRepository)
    {
        this.commandFactory = commandFactory;
        this.funcionarioRepository = funcionarioRepository;
    }

    public void CreateFor(Venda venda)
    {
        foreach (var funcionario in venda.Funcionarios)
            Create(funcionario, venda);
    }

    public IReadOnlyCollection<Funcionario> GetFor(Venda venda)
    {
        using var command = commandFactory.Create("SELECT * FROM Venda_Funcionarios WHERE Id_Venda = @Id_Venda");
        command.AddParameter("@Id_Venda", venda.Id);
        using var reader = command.ExecuteReader();

        var funcionarios = new List<Funcionario>();
        while (reader.Read())
            funcionarios.Add(funcionarioRepository.Get(reader.GetInt32(2)));
        return funcionarios;
    }

    public void UpdateFor(Venda venda)
    {
        DeleteFor(venda);
        CreateFor(venda);
    }

    public void DeleteFor(Venda venda)
    {
        using var command = commandFactory.Create("DELETE FROM Venda_Funcionarios WHERE Id_Venda = @Id_Venda");
        command.AddParameter("@Id_Venda", venda.Id);
        command.ExecuteNonQuery();
    }

    private void Create(Funcionario funcionario, Venda venda)
    {
        using var command = commandFactory.Create("INSERT INTO Venda_Funcionarios (Id_Venda, Id_Funcionario) VALUES (@Id_Venda, @Id_Funcionario)");
        command.AddParameter("@Id_Venda", venda.Id);
        command.AddParameter("@Id_Funcionario", funcionario.Id);
        command.ExecuteNonQuery();
    }
}