using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ISqlCommandFactory sqlCommandFactory;

    public UsuarioRepository(ISqlCommandFactory sqlCommandFactory)
    {
        this.sqlCommandFactory = sqlCommandFactory;
    }

    public Usuario Create(Usuario usuario)
    {
        using var command = sqlCommandFactory.Create("INSERT INTO Usuarios(Login, Data_Cadastro, Ativo) OUTPUT INSERTED.Id VALUES (@Login, @Data_Cadastro, @Ativo)");
        command.AddParameter("@Login", usuario.Login);
        command.AddParameter("@Data_Cadastro", usuario.DataCadastro);
        command.AddParameter("@Ativo", usuario.Ativo);

        var id = (int)command.ExecuteScalar()!;

        return usuario with { Id = id };
    }

    public Usuario Get(int id)
    {
        using var command = sqlCommandFactory.Create("SELECT (Id, Login, Data_Cadastro, Ativo) FROM Usuarios WHERE Id = @Id");
        command.AddParameter("@Id", id);
        using var reader = command.ExecuteReader();

        if (reader.Read())
            return CreateUsuario(reader);
        return new();
    }

    public IEnumerable<Usuario> Get()
    {
        using var command = sqlCommandFactory.Create("SELECT (Id, Login, Data_Cadastro, Ativo) FROM Usuarios");
        using var reader = command.ExecuteReader();

        while (reader.Read())
            yield return CreateUsuario(reader);
    }

    public void Update(Usuario usuario)
    {
        using var command = sqlCommandFactory.Create("UPDATE Usuarios SET Login = @Login, Data_Cadastro = @Data_Cadastro, Ativo = @Ativo WHERE Id = @Id");
        command.AddParameter("@Id", usuario.Id);
        command.AddParameter("@Login", usuario.Login);
        command.AddParameter("@Data_Cadastro", usuario.DataCadastro);
        command.AddParameter("@Ativo", usuario.Ativo);

        command.ExecuteNonQuery();
    }

    public void UpdatePassword(Usuario usuario, string password)
    {
        using var command = sqlCommandFactory.Create("UPDATE Usuarios SET Senha = @Senha WHERE Id = @Id");
        command.AddParameter("@Id", usuario.Id);
        command.AddParameter("@Senha", password);

        command.ExecuteNonQuery();
    }

    public void Delete(Usuario usuario)
    {
        using var command = sqlCommandFactory.Create("DELETE FROM Usuarios WHERE Id = @Id");
        command.AddParameter("@Id", usuario.Id);
        command.ExecuteNonQuery();
    }

    private static Usuario CreateUsuario(SqlDataReader reader)
    {
        return new()
        {
            Id = (int)reader["Id"],
            Login = (string)reader["Login"],
            DataCadastro = (DateOnly)reader["Data_Cadastro"],
            Ativo = (bool)reader["Ativo"]
        };
    }
}
