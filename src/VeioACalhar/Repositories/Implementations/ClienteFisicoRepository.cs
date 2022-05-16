using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class ClienteFisicoRepository : IClienteFisicoRepository
{
    private readonly IPessoaFisicaRepository<ClienteFisico> pessoaRepository;

    public ClienteFisicoRepository(IPessoaFisicaRepository<ClienteFisico> pessoaRepository)
    {
        this.pessoaRepository = pessoaRepository;
    }

    public ClienteFisico Create(ClienteFisico cliente)
    {
        return pessoaRepository.Create(cliente);
    }

    public ClienteFisico Get(int id)
    {
        return pessoaRepository.Get(id);
    }

    public IEnumerable<ClienteFisico> GetAll()
    {
        return pessoaRepository.GetAll();
    }

    public ClienteFisico Update(ClienteFisico cliente)
    {
        return pessoaRepository.Update(cliente);
    }

    public void Delete(ClienteFisico cliente)
    {
        pessoaRepository.Delete(cliente);
    }
}
