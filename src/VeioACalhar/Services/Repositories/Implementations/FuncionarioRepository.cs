using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class FuncionarioRepository : IFuncionarioRepository
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly ICargoRepository cargoRepository;
    private readonly IUsuarioRepository usuarioRepository;
    private readonly IPessoaFisicaRepository<Funcionario> pessoaFisicaRepository;

    public FuncionarioRepository(ISqlCommandFactory commandFactory, ICargoRepository cargoRepository, IUsuarioRepository usuarioRepository, IPessoaFisicaRepository<Funcionario> pessoaFisicaRepository)
    {
        this.commandFactory = commandFactory;
        this.cargoRepository = cargoRepository;
        this.usuarioRepository = usuarioRepository;
        this.pessoaFisicaRepository = pessoaFisicaRepository;
    }

    public Funcionario Create(Funcionario funcionario)
    {
        funcionario = pessoaFisicaRepository.Create(funcionario);

        using var command = commandFactory.Create("INSERT INTO Funcionarios(Id, Id_Cargo, Id_Usuario, Salario) VALUES (@Id, @Id_Cargo, @Id_Usuario, @Salario)");
        command.AddParameter("@Id", funcionario.Id);
        command.AddParameter("@Id_Cargo", funcionario.Cargo.Id);
        command.AddParameter("@Id_Usuario", funcionario.Usuario.Id);
        command.AddParameter("@Salario", funcionario.Salario);

        command.ExecuteNonQuery();

        return funcionario;
    }

    public Funcionario Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Funcionarios WHERE Id = @Id");
        command.AddParameter("@Id", id);
        using var reader = command.ExecuteReader();

        if (reader.Read())
            return CreateFuncionario(reader);
        return new();
    }

    public IReadOnlyCollection<Funcionario> GetAll()
    {
        using var command = commandFactory.Create("SELECT * FROM Funcionarios");
        using var reader = command.ExecuteReader();

        var funcionarios = new List<Funcionario>();
        while (reader.Read())
            funcionarios.Add(CreateFuncionario(reader));
        return funcionarios;
    }

    public Funcionario Update(Funcionario funcionario)
    {
        funcionario = pessoaFisicaRepository.Update(funcionario);

        using var command = commandFactory.Create("UPDATE Funcionarios SET Id_Cargo = @Id_Cargo, Id_Usuario = @Id_Usuario, Salario = @Salario WHERE Id = @Id");
        command.AddParameter("@Id", funcionario.Id);
        command.AddParameter("@Id_Cargo", funcionario.Cargo.Id);
        command.AddParameter("@Id_Usuario", funcionario.Usuario.Id);
        command.AddParameter("@Salario", funcionario.Salario);
        command.ExecuteNonQuery();

        return funcionario;
    }

    public void Delete(Funcionario funcionario)
    {
        using var command = commandFactory.Create("DELETE FROM Funcionarios WHERE Id = @Id");
        command.AddParameter("@Id", funcionario.Id);
        command.ExecuteNonQuery();

        pessoaFisicaRepository.Delete(funcionario);
    }

    private Funcionario CreateFuncionario(SqlDataReader reader)
    {
        var id = reader.GetInt32(0);
        var cargo = pessoaFisicaRepository.Get(id);
        return cargo with
        {
            Cargo = cargoRepository.Get(reader.GetInt32(1)),
            Usuario = usuarioRepository.Get(reader.GetInt32(2)),
            Salario = reader.GetSqlMoney(3).ToDecimal()
        };
    }
}