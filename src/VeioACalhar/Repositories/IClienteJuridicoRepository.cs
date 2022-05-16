using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IClienteJuridicoRepository
{
    ClienteJuridico Create(ClienteJuridico cliente);

    ClienteJuridico Get(int id);

    IEnumerable<ClienteJuridico> GetAll();

    ClienteJuridico Update(ClienteJuridico cliente);

    void Delete(ClienteJuridico cliente);
}
