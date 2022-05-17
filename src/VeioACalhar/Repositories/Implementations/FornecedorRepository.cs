using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

//todo: fazer sql para esse repositorio
public class FornecedorRepository : IFornecedorRepository
{
    private readonly IPessoaJuridicaRepository<Fornecedor> pessoaRepository;

    public FornecedorRepository(IPessoaJuridicaRepository<Fornecedor> pessoaRepository)
    {
        this.pessoaRepository = pessoaRepository;
    }

    public Fornecedor Create(Fornecedor fornecedor)
    {
        return pessoaRepository.Create(fornecedor);
    }

    public Fornecedor Get(int id)
    {
        return pessoaRepository.Get(id);
    }

    public IReadOnlyCollection<Fornecedor> GetAll()
    {
        return pessoaRepository.GetAll();
    }

    public Fornecedor Update(Fornecedor fornecedor)
    {
        return pessoaRepository.Update(fornecedor);
    }

    public void Delete(Fornecedor fornecedor)
    {
        pessoaRepository.Delete(fornecedor);
    }
}
