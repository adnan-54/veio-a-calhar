using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class FuncionarioRepository : IFuncionarioRepository
{
    private readonly ISqlCommandFactory commandFactory;
    private readonly IPessoaFisicaRepository pessoaFisicaRepository;
    private readonly ICargoRepository cargoRepository;
    private readonly IUsuarioRepository usuarioRepository;

    public FuncionarioRepository(ISqlCommandFactory commandFactory, IPessoaFisicaRepository pessoaFisicaRepository, ICargoRepository cargoRepository, IUsuarioRepository usuarioRepository)
    {
        this.commandFactory = commandFactory;
        this.pessoaFisicaRepository = pessoaFisicaRepository;
        this.cargoRepository = cargoRepository;
        this.usuarioRepository = usuarioRepository;
    }

    public Funcionario Create(Funcionario funcionario)
    {
        var pessoaFisica = pessoaFisicaRepository.Create(funcionario);
        using var command = commandFactory.Create("INSERT INTO Funcionarios(Id, Id_Cargo, Id_Usuario, Salario) VALUES (@Id, @Id_Cargo, @Id_Usuario, @Salario)");
        command.AddParameter("@Id", pessoaFisica.Id);
        command.AddParameter("@Id_Cargo", funcionario.Cargo.Id);
        command.AddParameter("@Id_Usuario", funcionario.Usuario.Id);
        command.AddParameter("@Salario", funcionario.Salario);
        command.ExecuteNonQuery();

        return funcionario with { Id = pessoaFisica.Id };
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

    public IEnumerable<Funcionario> Get()
    {
        using var command = commandFactory.Create("SELECT * FROM Funcionarios");
        using var reader = command.ExecuteReader();
        while (reader.Read())
            yield return CreateFuncionario(reader);
    }

    public void Update(Funcionario funcionario)
    {
        using var command = commandFactory.Create("UPDATE Funcionarios SET Id_Cargo = @Id_Cargo, Id_Usuario = @Id_Usuario, Salario = @Salario WHERE Id = @Id");
        command.AddParameter("@Id", funcionario.Id);
        command.AddParameter("@Id_Cargo", funcionario.Cargo.Id);
        command.AddParameter("@Id_Usuario", funcionario.Usuario.Id);
        command.AddParameter("@Salario", funcionario.Salario);
        command.ExecuteNonQuery();

        pessoaFisicaRepository.Update(funcionario);
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
        var id = (int)reader["Id"];
        var cargo = cargoRepository.Get((int)reader["Id_Cargo"]);
        var usuario = usuarioRepository.Get((int)reader["Id_Usuario"]);
        var salario = (decimal)reader["Salario"];

        var pessoaFisica = pessoaFisicaRepository.Get(id);

        return new()
        {
            Id = id,
            Cargo = cargo,
            Usuario = usuario,
            Salario = salario,
            Cpf = pessoaFisica.Cpf,
            Rg = pessoaFisica.Rg,
            Nome = pessoaFisica.Nome,
            Observacoes = pessoaFisica.Observacoes,
            Pix = pessoaFisica.Pix,
            Email = pessoaFisica.Email,
            Enderecos = pessoaFisica.Enderecos,
            Telefones = pessoaFisica.Telefones
        };
    }
}
