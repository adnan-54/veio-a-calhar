using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IUsuarioRepository
{
    Usuario Create(Usuario usuario);

    Usuario Get(int id);

    IEnumerable<Usuario> Get();

    Usuario Update(Usuario usuario);

    Usuario UpdatePassword(Usuario usuario, string password);

    void Delete(Usuario usuario);
}