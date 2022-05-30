using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ISqlCommandFactory commandFactory;

    public UsuarioRepository(ISqlCommandFactory commandFactory)
    {
        this.commandFactory = commandFactory;
    }

    public Usuario Create(Usuario usuario, string senha)
    {
        using var command = commandFactory.Create("INSERT INTO Usuarios(Login, Senha, Data_Cadastro, Ativo) OUTPUT INSERTED.Id VALUES (@Login, @Senha, @Data_Cadastro, @Ativo)");
        command.AddParameter("@Login", usuario.Login);
        command.AddParameter("@Senha", senha);
        command.AddParameter("@Data_Cadastro", DateTime.Today);
        command.AddParameter("@Ativo", true);

        var id = command.ExecuteScalar<int>();

        return usuario with { Id = id, DataCadastro = DateOnly.FromDateTime(DateTime.Today), Ativo = true };
    }

    public Usuario Get(int id)
    {
        using var command = commandFactory.Create("SELECT * FROM Usuarios WHERE Id = @Id");
        command.AddParameter("@Id", id);
        using var reader = command.ExecuteReader();

        if (reader.Read())
            return CreateUsuario(reader);
        return new();
    }

    public Usuario Get(string login)
    {
        using var command = commandFactory.Create("SELECT * FROM Usuarios WHERE Login = @Login AND Ativo = @Ativo");
        command.AddParameter("@Login", login);
        command.AddParameter("@Ativo", true);
        using var reader = command.ExecuteReader();

        if (reader.Read())
            return CreateUsuario(reader);
        return new();
    }

    public IReadOnlyCollection<Usuario> GetAll()
    {
        using var command = commandFactory.Create("SELECT * FROM Usuarios");
        using var reader = command.ExecuteReader();

        var usuarios = new List<Usuario>();
        while (reader.Read())
            usuarios.Add(CreateUsuario(reader));
        return usuarios;
    }

    public Usuario Update(Usuario usuario)
    {
        using var command = commandFactory.Create("UPDATE Usuarios SET Login = @Login, Ativo = @Ativo WHERE Id = @Id");
        command.AddParameter("@Id", usuario.Id);
        command.AddParameter("@Login", usuario.Login);
        command.AddParameter("@Ativo", usuario.Ativo);
        command.ExecuteNonQuery();

        return usuario;
    }

    public void Delete(Usuario usuario)
    {
        using var command = commandFactory.Create("DELETE FROM Usuarios WHERE Id = @Id");
        command.AddParameter("@Id", usuario.Id);
        command.ExecuteNonQuery();
    }

    private static Usuario CreateUsuario(SqlDataReader reader)
    {
        return new()
        {
            Id = reader.GetInt32(0),
            Login = reader.GetString(1),
            Password = reader.GetString(2),
            DataCadastro = DateOnly.FromDateTime(reader.GetDateTime(3)),
            Ativo = reader.GetBoolean(4)
        };
    }
}
