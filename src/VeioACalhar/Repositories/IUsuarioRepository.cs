using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IUsuarioRepository
{
    Usuario Create(Usuario usuario, string senha);

    Usuario Get(int id);

    Usuario Get(string login, string senha);

    IReadOnlyCollection<Usuario> GetAll();

    Usuario Update(Usuario usuario);

    void Delete(Usuario usuario);
}
