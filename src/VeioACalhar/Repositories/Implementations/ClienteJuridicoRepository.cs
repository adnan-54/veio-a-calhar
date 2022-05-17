using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

//todo: fazer sql para esse repositorio
public class ClienteJuridicoRepository : IClienteJuridicoRepository
{
    private readonly IPessoaJuridicaRepository<ClienteJuridico> pessoaRepository;

    public ClienteJuridicoRepository(IPessoaJuridicaRepository<ClienteJuridico> pessoaRepository)
    {
        this.pessoaRepository = pessoaRepository;
    }

    public ClienteJuridico Create(ClienteJuridico cliente)
    {
        return pessoaRepository.Create(cliente);
    }

    public ClienteJuridico Get(int id)
    {
        return pessoaRepository.Get(id);
    }

    public IReadOnlyCollection<ClienteJuridico> GetAll()
    {
        return pessoaRepository.GetAll();
    }

    public ClienteJuridico Update(ClienteJuridico cliente)
    {
        return pessoaRepository.Update(cliente);
    }

    public void Delete(ClienteJuridico cliente)
    {
        pessoaRepository.Delete(cliente);
    }
}
