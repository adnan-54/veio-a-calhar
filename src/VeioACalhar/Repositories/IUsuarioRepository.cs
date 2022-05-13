using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IUsuarioRepository
{
    Usuario Create(Usuario usuario);

    Usuario Get(int id);

    IEnumerable<Usuario> Get();

    void Update(Usuario usuario);

    void UpdatePassword(Usuario usuario, string password);

    void Delete(Usuario usuario);
}