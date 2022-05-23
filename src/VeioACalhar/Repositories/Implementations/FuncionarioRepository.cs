using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class FuncionarioRepository : IFuncionarioRepository
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly ICargoRepository cargoRepository;
    private readonly IUsuarioRepository usuarioRepository;

    public FuncionarioRepository(ISqlCommandFactory commandFactory, ICargoRepository cargoRepository, IUsuarioRepository usuarioRepository)
    {
        this.commandFactory = commandFactory;
        this.cargoRepository = cargoRepository;
        this.usuarioRepository = usuarioRepository;
    }

    public Funcionario Create(Funcionario funcionario)
    {
        using var command = commandFactory.Create("INSERT INTO Funcionarios(Id_Cargo, Id_Usuario, Salario) OUTPUT INSERTED.Id VALUES (@Id_Cargo, @Id_Usuario, @Salario)");
        command.AddParameter("@Id_Cargo", funcionario.Cargo.Id);
        command.AddParameter("@Id_Usuario", funcionario.Usuario.Id);
        command.AddParameter("@Salario", funcionario.Salario);

        var id = command.ExecuteScalar<int>();

        return funcionario with { Id = id };
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
    }

    private Funcionario CreateFuncionario(SqlDataReader reader)
    {
        return new()
        {
            Id = reader.GetInt32(0),
            Cargo = cargoRepository.Get(reader.GetInt32(1)),
            Usuario = usuarioRepository.Get(reader.GetInt32(2)),
            Salario = reader.GetDecimal(3)
        };
    }
}