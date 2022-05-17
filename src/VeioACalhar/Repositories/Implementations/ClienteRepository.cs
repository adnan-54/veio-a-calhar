using System.Linq;
using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly IClienteFisicoRepository clienteFisicoRepository;
    private readonly IClienteJuridicoRepository clienteJuridicoRepository;

    public ClienteRepository(IClienteFisicoRepository clienteFisicoRepository, IClienteJuridicoRepository clienteJuridicoRepository)
    {
        this.clienteFisicoRepository = clienteFisicoRepository;
        this.clienteJuridicoRepository = clienteJuridicoRepository;
    }

    public Pessoa Create(Pessoa cliente)
    {
        if (cliente is ClienteFisico clienteFisico)
            return clienteFisicoRepository.Create(clienteFisico);
        if (cliente is ClienteJuridico clienteJuridico)
            return clienteJuridicoRepository.Create(clienteJuridico);

        throw new Exception("Cliente invalido");
    }

    public Pessoa Get(int id)
    {
        var clienteFisico = clienteFisicoRepository.Get(id);
        if (clienteFisico.Id == id)
            return clienteFisico;

        var clienteJuridico = clienteJuridicoRepository.Get(id);
        if (clienteJuridico.Id == id)
            return clienteJuridico;

        throw new Exception("Cliente não encontrado");
    }

    public IReadOnlyCollection<Pessoa> GetAll()
    {
        return clienteFisicoRepository.GetAll().Cast<Pessoa>().Concat(clienteJuridicoRepository.GetAll().Cast<Pessoa>()).OrderBy(pessoa => pessoa.Nome).ToList();
    }

    public Pessoa Update(Pessoa cliente)
    {
        if (cliente is ClienteFisico clienteFisico)
            return clienteFisicoRepository.Update(clienteFisico);
        if (cliente is ClienteJuridico clienteJuridico)
            return clienteJuridicoRepository.Update(clienteJuridico);

        throw new Exception("Cliente invalido");
    }

    public void Delete(Pessoa cliente)
    {
        if (cliente is ClienteFisico clienteFisico)
            clienteFisicoRepository.Delete(clienteFisico);
        if (cliente is ClienteJuridico clienteJuridico)
            clienteJuridicoRepository.Delete(clienteJuridico);

        throw new Exception("Cliente invalido");
    }
}
